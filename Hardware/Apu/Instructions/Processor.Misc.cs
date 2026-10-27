namespace Hardware.Apu;

using static Operand;

public partial class Processor
{
    private void Nop()
    {
        // Dummy Read
        DummyRead();
    }
}
