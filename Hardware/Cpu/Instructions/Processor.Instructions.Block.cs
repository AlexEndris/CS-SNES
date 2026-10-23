namespace Hardware.Cpu;

public partial class Processor
{
    private void Nop(Operand _)
    {
        // Nothing
    }
    
    private void Wdm(Operand _)
    {
        cpuBus.Idle();
    }
}
