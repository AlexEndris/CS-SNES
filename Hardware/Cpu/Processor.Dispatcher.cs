namespace Hardware.Cpu;

public partial class Processor
{
    private void Execute(byte opcode)
    {
        switch (opcode)
        {
            case 0x42: Wdm(Immediate8()); break;
            case 0x64: Stz(Direct()); break;
            case 0x74: Stz(DirectX()); break;
            case 0x81: Sta(DirectIndexedIndirect()); break;
            case 0x83: Sta(StackRelative()); break;
            case 0x84: Sty(Direct()); break;
            case 0x85: Sta(Direct()); break;
            case 0x86: Stx(Direct()); break;
            case 0x87: Sta(DirectIndirectLong()); break;
            case 0x8C: Sty(Absolute()); break;
            case 0x8D: Sta(Absolute()); break;
            case 0x8E: Stx(Absolute()); break;
            case 0x8F: Sta(AbsoluteLong()); break;
            case 0x91: Sta(DirectIndirectY(true)); break;
            case 0x92: Sta(DirectIndirect()); break;
            case 0x93: Sta(StackRelativeIndirectY()); break;
            case 0x94: Sty(DirectX()); break;
            case 0x95: Sta(DirectX()); break;
            case 0x96: Stx(DirectY()); break;
            case 0x97: Sta(DirectIndirectLongY()); break;
            case 0x99: Sta(AbsoluteY(true)); break;
            case 0x9C: Stz(Absolute()); break;
            case 0x9D: Sta(AbsoluteX(true)); break;
            case 0x9E: Stz(AbsoluteX(true)); break;
            case 0x9F: Sta(AbsoluteLongX()); break;
            case 0xA0: Ldy(ImmediateX()); break;
            case 0xA1: Lda(DirectIndexedIndirect()); break;
            case 0xA2: Ldx(ImmediateX()); break;
            case 0xA3: Lda(StackRelative()); break;
            case 0xA4: Ldy(Direct()); break;
            case 0xA5: Lda(Direct()); break;
            case 0xA6: Ldx(Direct()); break;
            case 0xA7: Lda(DirectIndirectLong()); break;
            case 0xA9: Lda(ImmediateM()); break;
            case 0xAC: Ldy(Absolute()); break;
            case 0xAD: Lda(Absolute()); break;
            case 0xAE: Ldx(Absolute()); break;
            case 0xAF: Lda(AbsoluteLong()); break;
            case 0xB1: Lda(DirectIndirectY()); break;
            case 0xB2: Lda(DirectIndirect()); break;
            case 0xB3: Lda(StackRelativeIndirectY()); break;
            case 0xB4: Ldy(DirectX()); break;
            case 0xB5: Lda(DirectX()); break;
            case 0xB6: Ldx(DirectY()); break;
            case 0xB7: Lda(DirectIndirectLongY()); break;
            case 0xB9: Lda(AbsoluteY()); break;
            case 0xBC: Ldy(AbsoluteX()); break;
            case 0xBD: Lda(AbsoluteX()); break;
            case 0xBE: Ldx(AbsoluteY()); break;
            case 0xBF: Lda(AbsoluteLongX()); break;
            case 0xC0: Cpy(ImmediateX()); break;
            case 0xC1: Cmp(DirectIndexedIndirect()); break;
            case 0xC3: Cmp(StackRelative()); break;
            case 0xC4: Cpy(Direct()); break;
            case 0xC5: Cmp(Direct()); break;
            case 0xC7: Cmp(DirectIndirectLong()); break;
            case 0xCC: Cpy(Absolute()); break;
            case 0xCD: Cmp(Absolute()); break;
            case 0xCF: Cmp(AbsoluteLong()); break;
            case 0xD1: Cmp(DirectIndirectY()); break;
            case 0xD2: Cmp(DirectIndirect()); break;
            case 0xD3: Cmp(StackRelativeIndirectY()); break;
            case 0xD5: Cmp(DirectX()); break;
            case 0xD7: Cmp(DirectIndirectLongY()); break;
            case 0xD9: Cmp(AbsoluteY()); break;
            case 0xDD: Cmp(AbsoluteX()); break;
            case 0xDF: Cmp(AbsoluteLongX()); break;
            case 0xE0: Cpx(ImmediateX()); break;
            case 0xE4: Cpx(Direct()); break;
            case 0xEC: Cpx(Absolute()); break;
            case 0xEA: Nop(Implied()); break;
            default:
                throw new NotImplementedException($"Opcode ${opcode:X2}");
        }
    }
}
