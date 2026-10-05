namespace Hardware.Cpu;

public partial class Processor
{
    private void Lda(Operand operand)
    {
        if (Registers.M8)
        {
            byte value = ReadByte(operand);
            Registers.A = (ushort)((Registers.A & 0xFF00) | value);
            SetZeroNegativeFlags(value);
        }
        else
        {
            ushort value = ReadWord(operand);
            Registers.A = value;
            SetZeroNegativeFlags(value);
        }
    }
    
    private void Ldx(Operand operand)
    {
        if (Registers.X8)
        {
            byte value = ReadByte(operand);
            Registers.X = (ushort)((Registers.X & 0xFF00) | value);
            SetZeroNegativeFlags(value);
        }
        else
        {
            ushort value = ReadWord(operand);
            Registers.X = value;
            SetZeroNegativeFlags(value);
        }
    }    
    
    private void Ldy(Operand operand)
    {
        if (Registers.X8)
        {
            byte value = ReadByte(operand);
            Registers.Y = (ushort)((Registers.Y & 0xFF00) | value);
            SetZeroNegativeFlags(value);
        }
        else
        {
            ushort value = ReadWord(operand);
            Registers.Y = value;
            SetZeroNegativeFlags(value);
        }
    }
}
