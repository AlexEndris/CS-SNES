namespace Hardware.Apu;

using static Operand;

public partial class Processor
{
    private void MovA(Operand operand)
    {
        byte value = ReadByte(operand);
        MovA(value);
    }

    private void MovAIndirect(Operand operand)
    {
        byte value = ReadByte(operand);
        Wait();
        MovA(value);
    }
    
    private void MovX(Operand operand)
    {
        byte value = ReadByte(operand);
        MovX(value);
    }
    
    private void MovXA()
    {
        DummyRead();
        MovX(Registers.A);
    }
    
    private void MovY(Operand operand)
    {
        byte value = ReadByte(operand);
        MovY(value);
    }
    
    private void MovYA()
    {
        DummyRead();
        MovY(Registers.A);
    }

    private void MovXSp()
    {
        DummyRead();
        MovX(Registers.SP);
    }
    
    private void MovSpX()
    {
        DummyRead();
        Registers.SP = Registers.X;
    }

    private void MovW(Operand operand)
    {
        ushort value = ReadWord(operand);
        Registers.YA = value;
        SetZeroNegativeFlags(Registers.YA);
    }
    
    private void MovWWrite(Operand operand)
    {
        WriteWord(operand, Registers.YA);
    }
    
    private void MovDpDp(Operand operand)
    {
        byte value = ReadByte(operand);
        
        byte address = FetchByte();
        byte page = Page();
        
        WriteByte(FixedPage(page, address), value);
    }
    
    private void MovDp(Operand operand)
    {
        byte value = ReadByte(operand);
        
        byte address = FetchByte();
        byte page = Page();

        Operand destination = FixedPage(page, address);
        DummyRead(destination);
        WriteByte(destination, value);
    }

    private void MovAWrite(Operand operand)
    {
        DummyRead(operand);
        WriteByte(operand, Registers.A);
    }   
    
    private void MovAIndirectWrite(Operand operand)
    {
        WriteByte(operand, Registers.A);
    }
    
    private void MovXWrite(Operand operand)
    {
        DummyRead(operand);
        WriteByte(operand, Registers.X);
    }   
    
    private void MovYWrite(Operand operand)
    {
        DummyRead(operand);
        WriteByte(operand, Registers.Y);
    }   
    
    private void MovA(byte value)
    {
        Registers.A = value;
        SetZeroNegativeFlags(Registers.A);
    }
    
    private void MovX(byte value)
    {
        Registers.X = value;
        SetZeroNegativeFlags(Registers.X);
    }
    
    private void MovY(byte value)
    {
        Registers.Y = value;
        SetZeroNegativeFlags(Registers.Y);
    }

    private void MovAX()
    {
        DummyRead();
        MovA(Registers.X);
    }
    
    private void MovAY()
    {
        DummyRead();
        MovA(Registers.Y);
    }
}
