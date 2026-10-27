namespace Hardware.Apu;

public record Registers
{
    public byte A { get; set; }
    public byte X { get; set; }
    public byte Y { get; set; }
    public ushort YA
    {
        get => (ushort)(Y << 8 | A);
        set
        {
            Y = (byte)(value >> 8);
            A = (byte)value;
        }
    }

    public ushort PC { get; set; }
    public byte SP { get; set; }
    
    private byte psw;
    public byte Psw { get => psw; set => SetPsw(value); }
    
    public bool Negative { get => GetFlag(Psw, Flags.Negative); set => psw = SetFlag(Psw, Flags.Negative, value); }
    public bool Overflow { get => GetFlag(Psw, Flags.Overflow); set => psw = SetFlag(Psw, Flags.Overflow, value); }
    public bool Page { get => GetFlag(Psw, Flags.Page); set => psw = SetFlag(Psw, Flags.Page, value); }
    public bool Break { get => GetFlag(Psw, Flags.Break); set => psw = SetFlag(Psw, Flags.Break, value); }
    public bool HalfCarry { get => GetFlag(Psw, Flags.HalfCarry); set => psw = SetFlag(Psw, Flags.HalfCarry, value); }
    public bool IrqDisable { get => GetFlag(Psw, Flags.IrqDisable); set => psw = SetFlag(Psw, Flags.IrqDisable, value); }
    public bool Zero { get => GetFlag(Psw, Flags.Zero); set => psw = SetFlag(Psw, Flags.Zero, value); }
    public bool Carry { get => GetFlag(Psw, Flags.Carry); set => psw = SetFlag(Psw, Flags.Carry, value); }

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
    
    private void SetPsw(byte value)
    {
        Negative = GetFlag(value, Flags.Negative);
        Overflow = GetFlag(value, Flags.Overflow);
        Page = GetFlag(value, Flags.Page);
        Break = GetFlag(value, Flags.Break);
        HalfCarry = GetFlag(value, Flags.HalfCarry);
        IrqDisable = GetFlag(value, Flags.IrqDisable);
        Zero = GetFlag(value, Flags.Zero);
        Carry = GetFlag(value, Flags.Carry);
    }
}
