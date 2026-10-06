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
}
