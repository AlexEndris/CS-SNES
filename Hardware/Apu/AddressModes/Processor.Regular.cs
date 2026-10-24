namespace Hardware.Apu;

using static Operand;

public partial class Processor
{
    private Operand Immediate()
    {
        ushort address = Registers.PC;
        Registers.PC++;
        return CrossPage(address);
    }
}
