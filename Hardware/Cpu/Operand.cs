namespace Hardware.Cpu;

public record struct Operand
{
    private Operand(uint Address, uint WrapMask)
    {
        this.Address = Address;
        this.WrapMask = WrapMask;
    }

    public uint Address { get; }

    public uint WrapMask { get; }

    public static Operand WithinPage(uint address) => new Operand(address, 0x0000FF);
    public static Operand WithinBank(uint address) => new Operand(address, 0x00FFFF);

    public static Operand CrossBank(uint address) => new Operand(address, 0xFFFFFF);

    public static Operand Nothing => new Operand(0, 0);
}
