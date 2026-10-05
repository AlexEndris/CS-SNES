namespace Hardware.Cpu;

public record Registers
{
    private byte p;
    public byte P { get => p; set => SetP(value); }
    public ushort A { get; set; }
    public ushort X { get; set; }
    public ushort Y { get; set; }
    public ushort S { get; set => field = EmulationMode ? (ushort)((value & 0xFF) | 0x0100) : value; }
    public ushort D { get; set; }

    public byte DL => (byte)(D & 0xFF);
    public byte DH => (byte)(D >> 8);
    public byte Pbr { get; set; } 
    public byte Dbr { get; set; }
    public ushort PC { get; set; }
    
    public uint ProgramAddress => (uint)(Pbr << 16 | PC);
    
    public byte B { get => (byte)(A >> 8); }
    
    // Flags
    public bool Negative { get => GetFlag(P, Flags.Negative); set => p = SetFlag(P, Flags.Negative, value); }
    public bool Overflow { get => GetFlag(P, Flags.Overflow); set => p = SetFlag(P, Flags.Overflow, value); }
    public bool M8 { get => GetFlag(P, Flags.ASize); set => p = SetFlag(P, Flags.ASize, value); }
    public bool X8
    {
        get => GetFlag(P, Flags.XSizeBreak);
        set
        {
            p = SetFlag(P, Flags.XSizeBreak, value);
            if (value)
            {
                X &= 0xFF;
                Y &= 0xFF;
            }
        }
    }

    public bool Decimal { get => GetFlag(P, Flags.Decimal); set => p = SetFlag(P, Flags.Decimal, value); }
    public bool IrqDisable { get => GetFlag(P, Flags.IrqDisable); set => p = SetFlag(P, Flags.IrqDisable, value); }
    public bool Zero { get => GetFlag(P, Flags.Zero); set => p = SetFlag(P, Flags.Zero, value); }
    public bool Carry { get => GetFlag(P, Flags.Carry); set => p = SetFlag(P, Flags.Carry, value); }

    public bool EmulationMode
    {
        get;
        set
        {
            field = value;
            if (value)
            {
                M8 = true;
                X8 = true;
                S = (ushort)((S & 0xFF) | 0x0100);
            }
        }
    }
    //public bool Break { get => GetFlag(P, CpuFlags.XSizeBreak); set => P = SetFlag(P, CpuFlags.XSizeBreak, value); }

    private static bool GetFlag(byte from, Flags flag)
    {
        byte mask = (byte)flag;
        return (from & mask) != 0;
    }

    private static byte SetFlag(byte from, Flags flag, bool value)
    {
        byte mask = (byte)flag;
        return (byte)(value ? (from | mask) : (from & ~mask));
    }
    
    private void SetP(byte value)
    {
        Negative = GetFlag(value, Flags.Negative);
        Overflow = GetFlag(value, Flags.Overflow);
        M8 = EmulationMode || GetFlag(value, Flags.ASize);
        X8 = EmulationMode || GetFlag(value, Flags.XSizeBreak);
        Decimal = GetFlag(value, Flags.Decimal);
        IrqDisable = GetFlag(value, Flags.IrqDisable);
        Zero = GetFlag(value, Flags.Zero);
        Carry = GetFlag(value, Flags.Carry);
    }
}
