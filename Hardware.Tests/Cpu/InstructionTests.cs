namespace Hardware.Tests.Cpu;

using AwesomeAssertions;
using AwesomeAssertions.Execution;

using Models;

using static TestDataHelper;

public class InstructionTests
{
    public static TheoryData<string> TestFiles => Directory.Exists(DataDirectory65618)
        ? new (Directory.GetFiles(DataDirectory65618, "*.json")
            .Select(f => Path.GetFileNameWithoutExtension(f)).Order())
        : throw new DirectoryNotFoundException(DataDirectory65618);

    private TestProcessor Processor { get; set; } = null!;

    private TestCpuBus CpuBus { get; set; } = null!;

    public InstructionTests()
    {
        Reset();
    }

    private void Reset()
    {
        CpuBus = new TestCpuBus();
        Processor = new TestProcessor(CpuBus);
    }

    [Theory, MemberData(nameof(TestFiles))]
    public async Task AllInstructions(string filename)
    {
        var testCases = ReadTestData<InstructionTestData>(DataDirectory65618, filename);
            
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
        var testCases = ReadTestData<InstructionTestData>(DataDirectory65618,$"{instruction}.{mode}");
        
        await foreach (var test in testCases)
        {
            TestInstruction(test);
        }
    }
    
    [Theory(), InlineData("54", "e", 1)]
    public async Task SingleCase(string instruction, string mode, int caseNumber)
    {
        var test = await ReadTestData<InstructionTestData>(DataDirectory65618, $"{instruction}.{mode}")
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
        while (CpuBus.Log.Count < expected)
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

    private void SetupCpu(CpuState state)
    {
        CpuBus.ResetBus();
        CpuBus.SetRam(state.Ram);
        Processor.SetState(state);
    }

    private void AssertCpu(string name, CpuState state, Cycle[] cycles)
    {
        using (new AssertionScope(name))
        {
            Processor.AssertState(name, state);
            CpuBus.AssertRam(name, state.Ram);
            CpuBus.AssertCycles(name, cycles);
        }
    }
}
