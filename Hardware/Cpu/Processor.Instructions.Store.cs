namespace Hardware.Cpu;

public partial class Processor
{
    private void Sta(Operand operand)
    {
        if (Registers.M8)
        {
            WriteByte(operand, (byte)Registers.A);
        }
        else
        {
            WriteWord(operand, Registers.A);
        }
    }
}
