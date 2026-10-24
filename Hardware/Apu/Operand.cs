namespace Hardware.Apu;

public record struct Operand
{
    private Operand(ushort Address, ushort WrapMask)
    {
        this.Address = Address;
        this.WrapMask = WrapMask;
    }

    public ushort Address { get; }

    public ushort WrapMask { get; }

    public static Operand WithinPage(ushort address) => new Operand(address, 0x00FF);
    public static Operand CrossPage(ushort address) => new Operand(address, 0xFFFF);
    public static Operand Nothing => new Operand(0, 0);
}
