namespace Hardware.Cpu;

public partial class Processor
{
    private void And(Operand operand)
    {
        if (Registers.M8)
        {
            byte value = ReadByte(operand);
            Registers.A &= (ushort)(value | 0xFF00);
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            ushort value = ReadWord(operand);
            Registers.A &= value;
            SetZeroNegativeFlags(Registers.A);
        }
    }
    
    private void Eor(Operand operand)
    {
        if (Registers.M8)
        {
            byte value = ReadByte(operand);
            Registers.A = (ushort)((Registers.A & 0xFF00) | ((Registers.A & 0x00FF) ^ value));
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            ushort value = ReadWord(operand);
            Registers.A ^= value;
            SetZeroNegativeFlags(Registers.A);
        }
    }
    
    private void Ora(Operand operand)
    {
        if (Registers.M8)
        {
            byte value = ReadByte(operand);
            Registers.A |= value;
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            ushort value = ReadWord(operand);
            Registers.A |= value;
            SetZeroNegativeFlags(Registers.A);
        }
    }
}
