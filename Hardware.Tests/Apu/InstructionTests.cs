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

    [Theory(Explicit = true), InlineData("a9")]
    public async Task Instruction(string instruction)
    {
        var testCases = ReadTestData<InstructionTestData>(DataDirectorySpc700,$"{instruction}");
        
        await foreach (var test in testCases)
        {
            TestInstruction(test);
        }
    }
    
    [Theory(), InlineData("00", 1)]
    public async Task SingleCase(string instruction, int caseNumber)
    {
        var test = await ReadTestData<InstructionTestData>(DataDirectorySpc700, $"{instruction}")
            .FirstAsync(t => t!.Name == $"{instruction} {caseNumber}");

        TestInstruction(test);
    }

    private void TestInstruction(InstructionTestData? test)
    {
        if (test is null)
        {
            test.Should().NotBeNull();
        }
            
        SetupCpu(test.Initial, test.Cycles);

        int expected = test.Cycles.Length;
        var steps = 0;

        using (new AssertionScope(test.Name))
        {
            while (ApuBus.Log.Count < expected)
            {
                Processor.Tick();

                steps++;
                if (steps > expected)
                {
                    break;
                }
            }
        }

        AssertCpu(test.Name, test.Final, test.Cycles);
    }

    private void SetupCpu(ApuState state, Cycle[] cycles)
    {
        ApuBus.ResetBus();
        ApuBus.SetRam(state.Ram);
        ApuBus.ExpectedReads = cycles.Where(c => c.Outputs == "read").Select(x => $"${x.Address:X4}").ToList();
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
