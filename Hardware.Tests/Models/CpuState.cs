namespace Hardware.Tests.Models;

using Cpu;

public class CpuState
{
    public byte P { get; set; }

    public ushort A { get; set; }

    public ushort X { get; set; }

    public ushort Y { get; set; }

    public ushort S { get; set; }

    public ushort D { get; set; }

    public byte Pbr { get; set; }

    public byte Dbr { get; set; }

    public ushort PC { get; set; }

    public bool EmulationMode { get; set; }

    public RamEntry[] Ram { get; set; }

    public static implicit operator Registers(CpuState state)
    {
        return new Registers
        {
            A = state.A,
            X = state.X,
            Y = state.Y,
            S = state.S,
            P = state.P,
            DBR = state.Dbr,
            PC = state.PC,
            PBR = state.Pbr,
            D = state.D,
            EmulationMode = state.EmulationMode
        };
    }
}
