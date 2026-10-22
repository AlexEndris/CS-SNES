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
            Hex(Registers.A, 4).Should().Be(Hex(state.A, 4), "A");
            Hex(Registers.X, 4).Should().Be(Hex(state.X, 4), "X");
            Hex(Registers.Y, 4).Should().Be(Hex(state.Y, 4), "Y");
            Hex(Registers.S, 4).Should().Be(Hex(state.S, 4), "S");
            Hex(Registers.D, 4).Should().Be(Hex(state.D, 4), "D");
            Hex(Registers.PC, 4).Should().Be(Hex(state.PC, 4), "PC");
            Hex(Registers.Pbr, 2).Should().Be(Hex(state.Pbr, 2), "PBR");
            Hex(Registers.Dbr, 2).Should().Be(Hex(state.Dbr, 2), "DBR");
            Registers.EmulationMode.Should().Be(state.EmulationMode);
            FormatFlags(Registers.P).Should().Be(FormatFlags(state.P), "P register");
        }
    }
    private static string Hex(uint value, int digits) => "$" + value.ToString("X" + digits);
    
    static string FormatFlags(byte p) =>
        string.Concat("NVMXDIZC".Select((c, i) => (p & (0x80 >> i)) != 0 ? c : '-'));
}
