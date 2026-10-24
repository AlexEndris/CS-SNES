namespace Hardware.Apu;

public partial class Processor(IApuBus apuBus)
{
    public Registers Registers { get; protected set; } = new Registers();
    
    public void Tick()
    {
        byte opcode = FetchByte();
        Execute(opcode);
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
}
