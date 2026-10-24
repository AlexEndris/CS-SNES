namespace Hardware.Tests.Apu.Models;

using Hardware.Apu;

public class ApuState
{
    public byte Psw { get; set; }

    public byte A { get; set; }

    public byte X { get; set; }

    public byte Y { get; set; }

    public ushort PC { get; set; }
    
    public RamEntry[] Ram { get; set; }

    public static implicit operator Registers(ApuState state)
    {
        return new Registers
        {
            A = state.A,
            X = state.X,
            Y = state.Y,
            PC = state.PC,
            Psw = state.Psw,
        };
    }
}
