namespace Hardware.Cpu;

public record struct Operand
{
    private Operand(uint Address, uint WrapMask)
    {
        this.Address = Address;
        this.WrapMask = WrapMask;
    }

    public uint Address { get; set; }

    public uint WrapMask { get; set; }

    public static Operand WithinBank(uint address) => new Operand(address, 0x00FFFF);

    public static Operand CrossBank(uint address) => new Operand(address, 0xFFFFFF);

    public static Operand Nothing => new Operand(0, 0);
}
