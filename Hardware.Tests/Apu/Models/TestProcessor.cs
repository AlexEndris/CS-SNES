namespace Hardware.Tests.Apu.Models;

using AwesomeAssertions;
using AwesomeAssertions.Execution;

using Hardware.Apu;

public class TestProcessor(IApuBus cpuBus) : Processor(cpuBus)
{
    public void SetState(ApuState state)
    {
        Registers = state;
    }

    public void AssertState(string name, ApuState state)
    {
        using (new AssertionScope(name))
        {
            Hex(Registers.A, 4).Should().Be(Hex(state.A, 4), "A");
            Hex(Registers.X, 4).Should().Be(Hex(state.X, 4), "X");
            Hex(Registers.Y, 4).Should().Be(Hex(state.Y, 4), "Y");
            Hex(Registers.PC, 4).Should().Be(Hex(state.PC, 4), "PC");
            FormatFlags(Registers.Psw).Should().Be(FormatFlags(state.Psw), "Psw register");
        }
    }
    private static string Hex(uint value, int digits) => "$" + value.ToString("X" + digits);
    
    static string FormatFlags(byte p) =>
        string.Concat("NVPBHIZC".Select((c, i) => (p & (0x80 >> i)) != 0 ? c : '-'));
}
