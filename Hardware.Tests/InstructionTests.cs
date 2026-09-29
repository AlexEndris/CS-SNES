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

    private TestCpu cpu;
    private TestBus bus;
    
    public InstructionTests()
    {
        bus = new TestBus();
        cpu = new TestCpu(bus);
    }

    [Theory, MemberData(nameof(TestFiles))]
    public async Task Test1(string filename)
    {
        var testCases = ReadTestData(filename);
            

        await foreach (var test in testCases)
        {
            if (test is null)
            {
                test.Should().NotBeNull();
            }
            
            SetupCpu(test.Initial);

            var cycles = 0;
            do
            {
                cpu.Tick();
                if (cpu.Cycle > 100)
                {
                    Assert.Fail($"Instruction never completed for {test.Name}");
                }
            } while (cpu.Cycle != 0);

            AssertCpu(test.Name, test.Final);
        }
    }

    private void AssertCpu(string name, CpuState state)
    {
        cpu.AssertState(name, state);
        bus.AssertRam(name, state.Ram);
    }

    private void SetupCpu(CpuState state)
    {
        bus.SetRam(state.Ram);
        cpu.SetState(state);
    }
}
