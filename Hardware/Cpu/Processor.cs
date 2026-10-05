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
        uint highAddress = (operand.Address & ~operand.WrapMask) | ((operand.Address + 1) & operand.WrapMask);
        byte highByte = cpuBus.Read(highAddress);
        
        return (ushort)(lowByte | (highByte << 8));
    }
    
    private uint ReadLong(Operand operand)
    {
        byte lowByte = cpuBus.Read(operand.Address);
        uint midAddress = (operand.Address & ~operand.WrapMask) | ((operand.Address + 1) & operand.WrapMask);
        byte midByte = cpuBus.Read(midAddress);
        uint highAddress = (operand.Address & ~operand.WrapMask) | ((operand.Address + 2) & operand.WrapMask);
        byte highByte = cpuBus.Read(highAddress);

        return (uint)(lowByte | (midByte << 8) |  (highByte << 16));
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
    
    private void SetZeroNegativeFlags(ushort value)
    {
        Registers.Zero = value == 0;
        Registers.Negative = (value & 0x8000) > 0;
    }
    
    private void SetZeroNegativeFlags(byte value)
    {
        Registers.Zero = value == 0;
        Registers.Negative = (value & 0x80) > 0;
    }
}
