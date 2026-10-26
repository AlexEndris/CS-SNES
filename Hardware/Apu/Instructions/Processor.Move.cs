namespace Hardware.Apu;

public partial class Processor
{
    private void MovA(Operand operand)
    {
        byte value = ReadByte(operand);
        MovA(value);
    }

    private void MovA(byte value)
    {
        Registers.A = value;
        SetZeroNegativeFlags(Registers.A);
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
