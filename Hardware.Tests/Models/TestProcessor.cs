namespace Hardware.Tests.Models;

using AwesomeAssertions;
using AwesomeAssertions.Execution;

using Cpu;

public class TestProcessor(ICpuBus cpuBus) : Processor(cpuBus)
{
    public void SetState(CpuState state)
    {
        Registers = state;
    }

    public void AssertState(string name, CpuState state)
    {
        using (new AssertionScope(name))
        {
            Registers.A.Should().Be(state.A);
            Registers.X.Should().Be(state.X);
            Registers.Y.Should().Be(state.Y);
            Registers.S.Should().Be(state.S);
            Registers.D.Should().Be(state.D);
            Registers.PC.Should().Be(state.PC);
            Registers.PBR.Should().Be(state.Pbr);
            Registers.DBR.Should().Be(state.Dbr);
            Registers.EmulationMode.Should().Be(state.EmulationMode);
            FormatFlags(Registers.P).Should().Be(FormatFlags(state.P), "P register");
        }
    }
    
    static string FormatFlags(byte p) =>
        string.Concat("NVMXDIZC".Select((c, i) => (p & (0x80 >> i)) != 0 ? c : '-'));
}
