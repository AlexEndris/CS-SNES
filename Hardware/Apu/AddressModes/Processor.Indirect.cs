namespace Hardware.Apu;

using static Operand;

public partial class Processor
{
    private Operand Indirect()
    {
        byte page = Page();
        DummyRead();
        return FixedPage(page, Registers.X);
    }
    
    private Operand IndirectInc(bool isWrite = false)
    {
        byte page = Page();
        DummyRead();
        if (isWrite)
            Wait();
        
        return FixedPage(page, Registers.X++);
    }

    private Operand IndirectX()
    {
        byte baseAddress = FetchByte();
        byte pointerAddress = (byte)(baseAddress + Registers.X);
        Wait();
        byte page = Page();
        var lowAddress = ReadByte(FixedPage(page, pointerAddress));
        var highAddress = ReadByte(FixedPage(page, (byte)(pointerAddress + 1)));
        
        return CrossPage((ushort)(highAddress << 8 | lowAddress));
    }    
    
    private Operand IndirectIndexedY(bool isWrite = false)
    {
        byte pointerAddress = FetchByte();
        byte page = Page();
        if (!isWrite)
        {
            Wait();
        }
        
        byte lowAddress = ReadByte(FixedPage(page, pointerAddress));
        byte highAddress = ReadByte(FixedPage(page, (byte)(pointerAddress + 1)));

        if (isWrite)
        {
            Wait();
        }
        
        ushort address = (ushort)((highAddress << 8 | lowAddress) + Registers.Y);
        return CrossPage(address);
    }
}
