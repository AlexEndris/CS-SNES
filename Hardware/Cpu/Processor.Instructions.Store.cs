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
    
    private void Stx(Operand operand)
    {
        if (Registers.X8)
        {
            WriteByte(operand, (byte)Registers.X);
        }
        else
        {
            WriteWord(operand, Registers.X);
        }
    }
    
    private void Sty(Operand operand)
    {
        if (Registers.X8)
        {
            WriteByte(operand, (byte)Registers.Y);
        }
        else
        {
            WriteWord(operand, Registers.Y);
        }
    }
    
    private void Stz(Operand operand)
    {
        if (Registers.M8)
        {
            WriteByte(operand, 0);
        }
        else
        {
            WriteWord(operand, 0);
        }
    }
}
