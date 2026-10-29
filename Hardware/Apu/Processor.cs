namespace Hardware.Apu;

public partial class Processor(IApuBus apuBus)
{
    public Registers Registers { get; protected set; } = new Registers();
    
    public void Tick()
    {
        byte opcode = FetchByte();
        Execute(opcode);
    }
    
    private byte Page()
    {
        byte page = (byte)(Registers.Page ? 1 : 0);
        return page;
    }
    
    private byte FetchByte()
    {
        byte value = apuBus.Read(Registers.PC);
        Registers.PC++;
        return value;
    }
    
    private byte ReadByte(Operand operand)
    {
        return apuBus.Read(operand.Address);
    }
    
    private ushort ReadWord(Operand operand)
    {
        byte lowByte = apuBus.Read(operand.Address);
        ushort highAddress = CalculateAddressWithWrapMask(operand, 1);
        Wait();
        byte highByte = apuBus.Read(highAddress);
        
        return (ushort)(lowByte | (highByte << 8));
    }
    
    private static ushort CalculateAddressWithWrapMask(Operand operand, int offset)
    {
        return (ushort)((operand.Address & ~operand.WrapMask) | ((operand.Address + offset) & operand.WrapMask));
    }
    
    private void WriteByte(Operand operand, byte value)
    {
        apuBus.Write(operand.Address, value);
    }
    
    private void WriteWord(Operand operand, ushort value)
    {
        DummyRead(operand);
        apuBus.Write(operand.Address, (byte) value);
        ushort highAddress = CalculateAddressWithWrapMask(operand, 1);
        apuBus.Write(highAddress, (byte) (value >> 8));
    }
    
    private void DummyRead()
    {
        apuBus.Read(Registers.PC);
    }
    
    private void DummyRead(Operand operand)
    {
        apuBus.Read(operand.Address);
    }
    
    private void Wait()
    {
        apuBus.Wait();
    }
    
    private void SetZeroFlag(ushort value)
    {
        Registers.Zero = value == 0;
    }

    private void SetNegativeFlag(byte value)
    {
        Registers.Negative = (value & 0x80) > 0;
    }

    private void SetNegativeFlag(ushort value)
    {
        Registers.Negative = (value & 0x8000) > 0;
    }

    private void SetZeroNegativeFlags(byte value)
    {
        SetZeroFlag(value);
        SetNegativeFlag(value);
    }
    
    private void SetZeroNegativeFlags(ushort value)
    {
        SetZeroFlag(value);
        SetNegativeFlag(value);
    }
}
