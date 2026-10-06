namespace Hardware.Cpu;

public partial class Processor
{
    private void Cmp(Operand operand)
    {
        if (Registers.M8)
        {
            byte value = ReadByte(operand);
            byte register = (byte)(Registers.A & 0xFF);
            byte result = (byte)(register - value);
            SetZeroNegativeFlags(result);
            SetCarryFlagForCmp(register, value);
        }
        else
        {
            ushort value = ReadWord(operand);
            ushort result = (ushort)(Registers.A - value);
            SetZeroNegativeFlags(result);
            SetCarryFlagForCmp(Registers.A, value);
        }
    }
    
    private void Cpx(Operand operand)
    {
        if (Registers.X8)
        {
            byte value = ReadByte(operand);
            byte register = (byte)(Registers.X & 0xFF);
            byte result = (byte)(register - value);
            SetZeroNegativeFlags(result);
            SetCarryFlagForCmp(register, value);
        }
        else
        {
            ushort value = ReadWord(operand);
            ushort result = (ushort)(Registers.X - value);
            SetZeroNegativeFlags(result);
            SetCarryFlagForCmp(Registers.X, value);
        }
    }    
    
    private void Cpy(Operand operand)
    {
        if (Registers.X8)
        {
            byte value = ReadByte(operand);
            byte register = (byte)(Registers.Y & 0xFF);
            byte result = (byte)(register - value);
            SetZeroNegativeFlags(result);
            SetCarryFlagForCmp(register, value);
        }
        else
        {
            ushort value = ReadWord(operand);
            ushort result = (ushort)(Registers.Y - value);
            SetZeroNegativeFlags(result);
            SetCarryFlagForCmp(Registers.Y, value);
        }
    }
}
