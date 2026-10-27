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

    private void WriteByte(Operand operand, byte value)
    {
        apuBus.Write(operand.Address, value);
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
    
    private void SetZeroFlag(byte value)
    {
        Registers.Zero = value == 0;
    }

    private void SetNegativeFlag(byte value)
    {
        Registers.Negative = (value & 0x80) > 0;
    }

    private void SetZeroNegativeFlags(byte value)
    {
        SetZeroFlag(value);
        SetNegativeFlag(value);
    }
}
