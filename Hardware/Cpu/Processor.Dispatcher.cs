namespace Hardware.Cpu;

public partial class Processor
{
    private void Execute(byte opcode)
    {
        switch (opcode)
        {
            case 0xA0: Ldy(ImmediateX()); break;
            case 0xA2: Ldx(ImmediateX()); break;
            case 0xA4: Ldy(DirectPage()); break;
            case 0xA5: Lda(DirectPage()); break;
            case 0xA6: Ldx(DirectPage()); break;
            case 0xA9: Lda(ImmediateM()); break;
            case 0xAC: Ldy(Absolute()); break;
            case 0xAD: Lda(Absolute()); break;
            case 0xAE: Ldx(Absolute()); break;
            case 0xAF: Lda(AbsoluteLong()); break;
            case 0xB4: Ldy(DirectPageX()); break;
            case 0xB5: Lda(DirectPageX()); break;
            case 0xB6: Ldx(DirectPageY()); break;
            case 0xB9: Lda(AbsoluteY()); break;
            case 0xBC: Ldy(AbsoluteX()); break;
            case 0xBD: Lda(AbsoluteX()); break;
            case 0xBE: Ldx(AbsoluteY()); break;
            case 0xBF: Lda(AbsoluteLongX()); break;
            case 0xEA: Nop(Implied()); break;
            default:
                throw new NotImplementedException($"Opcode ${opcode:X2}");
        }
    }
}
