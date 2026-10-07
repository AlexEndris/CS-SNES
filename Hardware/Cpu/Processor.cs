namespace Hardware.Cpu;

public partial class Processor(ICpuBus cpuBus)
{
    public Registers Registers { get; protected set; } = new Registers();
    
    public byte Cycle { get; private set; }

    public void Tick()
    {
        byte opcode = FetchByte();
        Execute(opcode);
    }
    
    private byte ReadByte(Operand operand)
    {
        return cpuBus.Read(operand.Address);
    }
    
    private ushort ReadWord(Operand operand)
    {
        byte lowByte = cpuBus.Read(operand.Address);
        uint highAddress = CalculateAddressWithWrapMask(operand, 1);
        byte highByte = cpuBus.Read(highAddress);
        
        return (ushort)(lowByte | (highByte << 8));
    }
    
    private uint ReadLong(Operand operand)
    {
        byte lowByte = cpuBus.Read(operand.Address);
        uint midAddress = CalculateAddressWithWrapMask(operand, 1);
        byte midByte = cpuBus.Read(midAddress);
        uint highAddress = CalculateAddressWithWrapMask(operand, 2);
        byte highByte = cpuBus.Read(highAddress);

        return (uint)(lowByte | (midByte << 8) |  (highByte << 16));
    }

    private static uint CalculateAddressWithWrapMask(Operand operand, int offset)
    {
        return (uint)((operand.Address & ~operand.WrapMask) | ((operand.Address + offset) & operand.WrapMask));
    }

    private void WriteByte(Operand operand, byte value)
    {
        cpuBus.Write(operand.Address, value);
    }

    private void WriteWord(Operand operand, ushort value)
    {
        cpuBus.Write(operand.Address, (byte)value);
        uint highAddress = CalculateAddressWithWrapMask(operand, 1);
        cpuBus.Write(highAddress, (byte)(value >> 8));
    }
    
    private void WriteWordRmw(Operand operand, ushort value)
    {
        uint highAddress = CalculateAddressWithWrapMask(operand, 1);
        cpuBus.Write(highAddress, (byte)(value >> 8));
        cpuBus.Write(operand.Address, (byte)value);
    }

    private byte FetchByte()
    {
        byte value = cpuBus.Read(Registers.ProgramAddress);
        Registers.PC++;
        return value;
    }
    
    private ushort FetchWord()
    {
        byte lowByte = FetchByte();
        byte highByte = FetchByte();
        return (ushort)((highByte << 8) | lowByte);
    }
    
    private uint FetchLong()
    {
        ushort word = FetchWord();
        byte longByte = FetchByte();
        return (uint)((longByte << 16) | word);
    }

    private void SetZeroFlag(ushort value)
    {
        Registers.Zero = value == 0;
    }
    
    private void SetZeroNegativeFlags(ushort value)
    {
        SetZeroFlag(value);
        Registers.Negative = (value & 0x8000) > 0;
    }
    
    private void SetZeroNegativeFlags(byte value)
    {
        SetZeroFlag(value);
        Registers.Negative = (value & 0x80) > 0;
    }

    private void SetCarryFlagForCmp(ushort original, ushort value)
    {
        Registers.Carry = original >= value;
    }
}
