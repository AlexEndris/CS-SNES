namespace Hardware;

public record struct CpuRegisters
{
    private byte p;
    public byte P { get => p; set => SetP(value); }
    public ushort A { get; set; }
    public ushort X { get; set; }
    public ushort Y { get; set; }
    public ushort S { get; set; }
    public ushort D { get; set; }
    public byte PBR { get; set; } 
    public byte DBR { get; set; }
    public ushort PC { get; set; }
    
    public byte B { get => (byte)(A >> 8); }
    
    // Flags
    public bool Negative { get => GetFlag(P, CpuFlags.Negative); set => p = SetFlag(P, CpuFlags.Negative, value); }
    public bool Overflow { get => GetFlag(P, CpuFlags.Overflow); set => p = SetFlag(P, CpuFlags.Overflow, value); }
    public bool ASize { get => GetFlag(P, CpuFlags.ASize); set => p = SetFlag(P, CpuFlags.ASize, value); }
    public bool XSize
    {
        get => GetFlag(P, CpuFlags.XSizeBreak);
        set
        {
            p = SetFlag(P, CpuFlags.XSizeBreak, value);
            if (value)
            {
                X &= 0xFF;
                Y &= 0xFF;
            }
        }
    }

    public bool Decimal { get => GetFlag(P, CpuFlags.Decimal); set => p = SetFlag(P, CpuFlags.Decimal, value); }
    public bool IrqDisable { get => GetFlag(P, CpuFlags.IrqDisable); set => p = SetFlag(P, CpuFlags.IrqDisable, value); }
    public bool Zero { get => GetFlag(P, CpuFlags.Zero); set => p = SetFlag(P, CpuFlags.Zero, value); }
    public bool Carry { get => GetFlag(P, CpuFlags.Carry); set => p = SetFlag(P, CpuFlags.Carry, value); }

    public bool EmulationMode
    {
        get;
        set
        {
            field = value;
            if (value)
            {
                ASize = true;
                XSize = true;
                S = (ushort)((S & 0xFF) | 0x0100);
            }
        }
    }
    //public bool Break { get => GetFlag(P, CpuFlags.XSizeBreak); set => P = SetFlag(P, CpuFlags.XSizeBreak, value); }

    private static bool GetFlag(byte from, CpuFlags flag)
    {
        byte mask = (byte)flag;
        return (from & mask) != 0;
    }

    private static byte SetFlag(byte from, CpuFlags flag, bool value)
    {
        byte mask = (byte)flag;
        return (byte)(value ? (from | mask) : (from & ~mask));
    }
    
    private void SetP(byte value)
    {
        Negative = GetFlag(value, CpuFlags.Negative);
        Overflow = GetFlag(value, CpuFlags.Overflow);
        ASize = EmulationMode || GetFlag(value, CpuFlags.ASize);
        XSize = EmulationMode || GetFlag(value, CpuFlags.XSizeBreak);
        Decimal = GetFlag(value, CpuFlags.Decimal);
        IrqDisable = GetFlag(value, CpuFlags.IrqDisable);
        Zero = GetFlag(value, CpuFlags.Zero);
        Carry = GetFlag(value, CpuFlags.Carry);
    }
}
