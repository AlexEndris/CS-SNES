namespace Hardware.Cpu;

public partial class Processor
{
    private void Clc(Operand _)
    {
        Registers.Carry = false;
    }    
    
    private void Cld(Operand _)
    {
        Registers.Decimal = false;
    }    
    
    private void Cli(Operand _)
    {
        Registers.IrqDisable = false;
    }    
    
    private void Clv(Operand _)
    {
        Registers.Overflow = false;
    }    
    
    private void Sec(Operand _)
    {
        Registers.Carry = true;
    }    
    
    private void Sed(Operand _)
    {
        Registers.Decimal = true;
    }    
    
    private void Sei(Operand _)
    {
        Registers.IrqDisable = true;
    }

    private void Rep(Operand operand)
    {
        byte value = ReadByte(operand);
        Registers.P &= (byte)~value;
        cpuBus.Idle();
    }

    private void Sep(Operand operand)
    {
        byte value = ReadByte(operand);
        Registers.P |= value;
        cpuBus.Idle();
    }

    private void Xce(Operand _)
    {
        (Registers.Carry, Registers.EmulationMode) = (Registers.EmulationMode, Registers.Carry);
    }
}
