namespace Hardware.Tests.Apu;

using AwesomeAssertions;
using AwesomeAssertions.Execution;

using Models;

using static TestDataHelper;

public class InstructionTests
{
    public static TheoryData<string> TestFiles => Directory.Exists(DataDirectorySpc700)
        ? new (Directory.GetFiles(DataDirectorySpc700, "*.json")
            .Select(f => Path.GetFileNameWithoutExtension(f)).Order())
        : throw new DirectoryNotFoundException(DataDirectorySpc700);

    private TestProcessor Processor { get; set; } = null!;

    private TestApuBus ApuBus { get; set; } = null!;

    public InstructionTests()
    {
        Reset();
    }

    private void Reset()
    {
        ApuBus = new TestApuBus();
        Processor = new TestProcessor(ApuBus);
    }

    [Theory, MemberData(nameof(TestFiles))]
    public async Task AllInstructions(string filename)
    {
        var testCases = ReadTestData<InstructionTestData>(DataDirectorySpc700, filename);
            
        await foreach (var test in testCases)
        {
            try
            {
                Reset();
                TestInstruction(test);
            }
            catch (NotImplementedException ex)
            {
                Assert.Skip($"Not implemented: {ex.Message}");
            }
        }
    }

    [Theory(Explicit = true), InlineData("a9", "e")]
    public async Task Instruction(string instruction, string mode)
    {
        var testCases = ReadTestData<InstructionTestData>(DataDirectorySpc700,$"{instruction}.{mode}");
        
        await foreach (var test in testCases)
        {
            TestInstruction(test);
        }
    }
    
    [Theory(), InlineData("54", "e", 1)]
    public async Task SingleCase(string instruction, string mode, int caseNumber)
    {
        var test = await ReadTestData<InstructionTestData>(DataDirectorySpc700, $"{instruction}.{mode}")
            .FirstAsync(t => t!.Name == $"{instruction} {mode} {caseNumber}");

        TestInstruction(test);
    }

    private void TestInstruction(InstructionTestData? test)
    {
        if (test is null)
        {
            test.Should().NotBeNull();
        }
            
        SetupCpu(test.Initial);

        int expected = test.Cycles.Length;
        var steps = 0;
        while (ApuBus.Log.Count < expected)
        {
            Processor.Tick();
            
            steps++;
            if (steps > expected)
            {
                Assert.Fail($"Instruction never completed for {test.Name}");
            }
        }

        AssertCpu(test.Name, test.Final, test.Cycles);
    }

    private void SetupCpu(ApuState state)
    {
        ApuBus.ResetBus();
        ApuBus.SetRam(state.Ram);
        Processor.SetState(state);
    }

    private void AssertCpu(string name, ApuState state, Cycle[] cycles)
    {
        using (new AssertionScope(name))
        {
            Processor.AssertState(name, state);
            ApuBus.AssertRam(name, state.Ram);
            ApuBus.AssertCycles(name, cycles);
        }
    }
}
