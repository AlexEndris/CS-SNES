namespace Hardware.Cpu;

public partial class Processor
{
    private void RolA(Operand _)
    {
        if (Registers.M8)
        {
            byte carry = (byte)(Registers.Carry ? 1 : 0);
            Registers.Carry = (Registers.A & 0x80) > 0;
            Registers.A = (ushort)((Registers.A & 0xFF00) | ((Registers.A << 1) & 0xFF) | carry);
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            ushort carry = (ushort)(Registers.Carry ? 1 : 0);
            Registers.Carry = (Registers.A & 0x8000) > 0;
            Registers.A = (ushort)(Registers.A << 1 | carry);
            SetZeroNegativeFlags(Registers.A);
        }
    }

    private void Rol(Operand operand)
    {
        if (Registers.M8)
        {
            byte data = ReadByte(operand);
            byte carry = (byte)(Registers.Carry ? 1 : 0);
            Registers.Carry = (data & 0x80) > 0;
            data = (byte)(data << 1 | carry);
            cpuBus.Idle();
            WriteByte(operand, data);
            SetZeroNegativeFlags(data);
        }
        else
        {
            ushort data = ReadWord(operand);
            ushort carry = (ushort)(Registers.Carry ? 1 : 0);
            Registers.Carry = (data & 0x8000) > 0;
            data = (ushort)(data << 1 | carry);
            cpuBus.Idle();
            WriteWordRmw(operand, data);
            SetZeroNegativeFlags(data);
        }
    }
    
    private void RorA(Operand _)
    {
        if (Registers.M8)
        {
            byte carry = (byte)(Registers.Carry ? 0x80 : 0);
            Registers.Carry = (Registers.A & 0x1) > 0;
            Registers.A = (ushort)((Registers.A & 0xFF00) | ((Registers.A & 0xFF) >> 1) | carry);
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            ushort carry = (ushort)(Registers.Carry ? 0x8000 : 0);
            Registers.Carry = (Registers.A & 0x1) > 0;
            Registers.A = (ushort)(Registers.A >> 1 | carry);
            SetZeroNegativeFlags(Registers.A);
        }
    }
    
    private void Ror(Operand operand)
    {
        if (Registers.M8)
        {
            byte data = ReadByte(operand);
            byte carry = (byte)(Registers.Carry ? 0x80 : 0);
            Registers.Carry = (data & 0x1) > 0;
            data = (byte)(data >> 1 | carry);
            cpuBus.Idle();
            WriteByte(operand, data);
            SetZeroNegativeFlags(data);
        }
        else
        {
            ushort data = ReadWord(operand);
            ushort carry = (ushort)(Registers.Carry ? 0x8000 : 0);
            Registers.Carry = (data & 0x1) > 0;
            data = (ushort)(data >> 1 | carry);
            cpuBus.Idle();
            WriteWordRmw(operand, data);
            SetZeroNegativeFlags(data);
        }
    }
}
