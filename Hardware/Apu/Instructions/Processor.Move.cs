namespace Hardware.Apu;

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
