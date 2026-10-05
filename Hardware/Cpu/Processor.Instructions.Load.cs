namespace Hardware.Cpu;

public partial class Processor
{
    private void Lda(Operand operand)
    {
        if (Registers.M8)
        {
            byte value =  cpuBus.Read(operand.Address);
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
}
