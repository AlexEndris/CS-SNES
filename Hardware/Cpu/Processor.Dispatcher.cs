namespace Hardware.Cpu;

public partial class Processor
{
    private void Execute(byte opcode)
    {
        switch (opcode)
        {
            case 0xEA: Nop(Implied()); break;
            case 0xA9: Lda(ImmediateM()); break;
        }
    }
}
