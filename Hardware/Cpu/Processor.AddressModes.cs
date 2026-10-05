namespace Hardware.Cpu;

using static Operand;

public partial class Processor
{
    private Operand Implied()
    {
        cpuBus.Idle();
        return Nothing;
    }

    private Operand ImmediateM()
    {
        uint address = Registers.ProgramAddress;
        Registers.PC += (ushort)(Registers.M8
            ? 1
            : 2);
        return WithinBank(address);
    }
}
