namespace Hardware.Apu;

using static Operand;

public partial class Processor
{
    private Operand Indirect()
    {
        byte page = (byte)(Registers.Page ? 1 : 0);
        DummyRead();
        return FixedPage(page, Registers.X);
    }
    
    private Operand IndirectInc(bool isWrite = false)
    {
        byte page = (byte)(Registers.Page ? 1 : 0);
        DummyRead();
        if (isWrite)
            Wait();
        
        return FixedPage(page, Registers.X++);
    }
}
