namespace Hardware.Tests.Models;

using AwesomeAssertions;

public class TestCpu(IBus Bus) : Cpu(Bus)
{
    public void SetState(CpuState state)
    {
        Registers = state;
    }

    public void AssertState(string name, CpuState state)
    {
        Registers.Should().Be(state, name);
    }
}
