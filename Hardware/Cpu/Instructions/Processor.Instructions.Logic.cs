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
            Registers.A ^= value;
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
    
    private void Bit(Operand operand)
    {
        if (Registers.M8)
        {
            byte value = ReadByte(operand);
            byte result = (byte)(Registers.A & value);
            SetZeroFlag(result);
            Registers.Negative = (value & 0x80) > 0;
            Registers.Overflow = (value & 0x40) > 0;
        }
        else
        {
            ushort value = ReadWord(operand);
            ushort result = (ushort)(Registers.A & value);
            SetZeroFlag(result);
            Registers.Negative = (value & 0x8000) > 0;
            Registers.Overflow = (value & 0x4000) > 0;
        }
    }
    
    private void BitImm(Operand operand)
    {
        if (Registers.M8)
        {
            byte value = ReadByte(operand);
            byte result = (byte)(Registers.A & value);
            SetZeroFlag(result);
        }
        else
        {
            ushort value = ReadWord(operand);
            ushort result = (ushort)(Registers.A & value);
            SetZeroFlag(result);
        }
    }
}
