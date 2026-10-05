namespace Hardware.Tests;

using AwesomeAssertions;

using Models;

using static TestDataHelper;

public class InstructionTests
{
    public static TheoryData<string> TestFiles => Directory.Exists(DataDirectory)
        ? new (Directory.GetFiles(DataDirectory, "*.json")
            .Select(f => Path.GetFileNameWithoutExtension(f)).Order())
        : throw new DirectoryNotFoundException(DataDirectory);

    private TestProcessor Processor { get; }

    private TestCpuBus CpuBus { get; }

    public InstructionTests()
    {
        CpuBus = new TestCpuBus();
        Processor = new TestProcessor(CpuBus);
    }

    [Theory, MemberData(nameof(TestFiles))]
    public async Task AllInstructions(string filename)
    {
        var testCases = ReadTestData(filename);
            
        await foreach (var test in testCases)
        {
            try
            {
                TestInstruction(test);
            }
            catch (NotImplementedException ex)
            {
                Assert.Skip($"Not implemented: {ex.Message}");
            }
        }
    }

    [Theory, InlineData("a9", "e")]
    public async Task Instruction(string instruction, string mode)
    {
        var testCases = ReadTestData($"{instruction}.{mode}");
        
        await foreach (var test in testCases)
        {
            TestInstruction(test);
        }
    }
    
    [Theory, InlineData("a9", "e", 1)]
    public async Task SingleCase(string instruction, string mode, int caseNumber)
    {
        var test = await ReadTestData($"{instruction}.{mode}")
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

        var cycles = 0;
        do
        {
            Processor.Tick();
            if (Processor.Cycle > 100)
            {
                Assert.Fail($"Instruction never completed for {test.Name}");
            }
        } while (Processor.Cycle != 0);

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
        Processor.AssertState(name, state);
        CpuBus.AssertRam(name, state.Ram);
        CpuBus.AssertCycles(name, cycles);
    }
}
