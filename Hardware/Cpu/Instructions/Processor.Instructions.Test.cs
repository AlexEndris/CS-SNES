namespace Hardware.Cpu;

public partial class Processor
{
    private void Trb(Operand operand)
    {
        if (Registers.M8)
        {
            byte data = ReadByte(operand);
            byte test = (byte)(data & Registers.A);
            SetZeroFlag(test);
            
            data &= (byte)~Registers.A;
            cpuBus.Idle();
            WriteByte(operand, data);
        }
        else
        {
            ushort data = ReadWord(operand);
            ushort test = (ushort)(data & Registers.A);
            SetZeroFlag(test);
            
            data &= (ushort)~Registers.A;
            cpuBus.Idle();
            WriteWordRmw(operand, data);
        }
    }
    
    private void Tsb(Operand operand)
    {
        if (Registers.M8)
        {
            byte data = ReadByte(operand);
            byte test = (byte)(data & Registers.A);
            SetZeroFlag(test);
            
            data |= (byte)Registers.A;
            cpuBus.Idle();
            WriteByte(operand, data);
        }
        else
        {
            ushort data = ReadWord(operand);
            ushort test = (ushort)(data & Registers.A);
            SetZeroFlag(test);

            data |= Registers.A;
            cpuBus.Idle();
            WriteWordRmw(operand, data);
        }
    }
}
