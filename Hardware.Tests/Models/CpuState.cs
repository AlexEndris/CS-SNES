namespace Hardware.Tests.Models;

public class CpuState : IEquatable<CpuRegisters>
{
    public byte P { get; set; }

    public ushort A { get; set; }

    public ushort X { get; set; }

    public ushort Y { get; set; }

    public ushort S { get; set; }

    public ushort D { get; set; }

    public byte PBR { get; set; }

    public byte DBR { get; set; }

    public ushort PC { get; set; }

    public bool EmulationMode { get; set; }

    public RamEntry[] Ram { get; set; }

    public static implicit operator CpuRegisters(CpuState state)
    {
        return new CpuRegisters
        {
            A = state.A,
            X = state.X,
            Y = state.Y,
            S = state.S,
            P = state.P,
            DBR = state.DBR,
            PC = state.PC,
            PBR = state.PBR,
            D = state.D,
            EmulationMode = state.EmulationMode
        };
    }

    public bool Equals(CpuRegisters other)
    {
        return P == other.P
               && A == other.A
               && X == other.X
               && Y == other.Y
               && S == other.S
               && D == other.D
               && PBR == other.PBR
               && DBR == other.DBR
               && PC == other.PC
               && EmulationMode == other.EmulationMode;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != GetType())
            return false;
        return Equals((CpuState)obj);
    }

    public override int GetHashCode()
    {
        HashCode hashCode = new HashCode();
        hashCode.Add(P);
        hashCode.Add(A);
        hashCode.Add(X);
        hashCode.Add(Y);
        hashCode.Add(S);
        hashCode.Add(D);
        hashCode.Add(PBR);
        hashCode.Add(DBR);
        hashCode.Add(PC);
        hashCode.Add(EmulationMode);
        hashCode.Add(Ram);
        return hashCode.ToHashCode();
    }
}
