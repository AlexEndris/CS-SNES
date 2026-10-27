namespace Hardware.Cpu;

public partial class Processor
{
    private void AslA(Operand _)
    {
        if (Registers.M8)
        {
            Registers.Carry = (Registers.A & 0x80) > 0;
            Registers.A = (ushort)((Registers.A & 0xFF00) | ((Registers.A << 1) & 0xFF));
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            Registers.Carry = (Registers.A & 0x8000) > 0;
            Registers.A <<= 1;
            SetZeroNegativeFlags(Registers.A);
        }
    }

    private void Asl(Operand operand)
    {
        if (Registers.M8)
        {
            byte data = ReadByte(operand);
            Registers.Carry = (data & 0x80) > 0;
            data <<= 1;
            Idle();
            WriteByte(operand, data);
            SetZeroNegativeFlags(data);
        }
        else
        {
            ushort data = ReadWord(operand);
            Registers.Carry = (data & 0x8000) > 0;
            data <<= 1;
            Idle();
            WriteWordRmw(operand, data);
            SetZeroNegativeFlags(data);
        }
    }

    private void LsrA(Operand _)
    {
        if (Registers.M8)
        {
            Registers.Carry = (Registers.A & 0x1) > 0;
            Registers.A = (ushort)((Registers.A & 0xFF00) | ((Registers.A & 0xFF) >> 1) );
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            Registers.Carry = (Registers.A & 0x1) > 0;
            Registers.A >>= 1;
            SetZeroNegativeFlags(Registers.A);
        }
    }
    
    private void Lsr(Operand operand)
    {
        if (Registers.M8)
        {
            byte data = ReadByte(operand);
            Registers.Carry = (data & 0x1) > 0;
            data >>= 1;
            Idle();
            WriteByte(operand, data);
            SetZeroNegativeFlags(data);
        }
        else
        {
            ushort data = ReadWord(operand);
            Registers.Carry = (data & 0x1) > 0;
            data >>= 1;
            Idle();
            WriteWordRmw(operand, data);
            SetZeroNegativeFlags(data);
        }
    }
}
