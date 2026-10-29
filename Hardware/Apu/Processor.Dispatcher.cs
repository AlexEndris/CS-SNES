namespace Hardware.Apu;

public partial class Processor
{
    private void Execute(byte opcode)
    {
        switch (opcode)
        {
            case 0x00: Nop(); break; 
            case 0x5D: MovXA(); break;
            case 0x7D: MovAX(); break;
            case 0x8D: MovY(Immediate()); break;
            case 0x8F: MovDp(Immediate()); break;
            case 0x9D: MovXSp(); break;
            case 0xBA: MovW(DirectPage()); break;
            case 0xBD: MovSpX(); break;
            case 0xAF: MovAIndirectWrite(IndirectInc(true)); break;
            case 0xBF: MovAIndirect(IndirectInc()); break;
            case 0xC4: MovAWrite(DirectPage()); break;
            case 0xC5: MovAWrite(Absolute()); break;
            case 0xC6: MovAWrite(Indirect()); break;
            case 0xC7: MovAWrite(IndirectX()); break;
            case 0xC9: MovXWrite(Absolute()); break;
            case 0xCB: MovYWrite(DirectPage()); break;
            case 0xCC: MovYWrite(Absolute()); break;
            case 0xCD: MovX(Immediate()); break;
            case 0xD4: MovAWrite(DirectPageX()); break;
            case 0xD5: MovAWrite(AbsoluteX()); break;
            case 0xD6: MovAWrite(AbsoluteY()); break;
            case 0xD7: MovAWrite(IndirectIndexedY(true)); break;
            case 0xD8: MovXWrite(DirectPage()); break;
            case 0xD9: MovXWrite(DirectPageY()); break;
            case 0xDA: MovWWrite(DirectPage()); break;
            case 0xDB: MovYWrite(DirectPageX()); break;
            case 0xDD: MovAY(); break;
            case 0xE4: MovA(DirectPage()); break;
            case 0xE5: MovA(Absolute()); break;
            case 0xE6: MovA(Indirect()); break;
            case 0xE7: MovA(IndirectX()); break;
            case 0xE8: MovA(Immediate()); break;
            case 0xE9: MovX(Absolute()); break;
            case 0xEB: MovY(DirectPage()); break;
            case 0xEC: MovY(Absolute()); break;
            case 0xF4: MovA(DirectPageX()); break;
            case 0xF5: MovA(AbsoluteX()); break;
            case 0xF6: MovA(AbsoluteY()); break;
            case 0xF7: MovA(IndirectIndexedY()); break;
            case 0xF8: MovX(DirectPage()); break;
            case 0xF9: MovX(DirectPageY()); break;
            case 0xFA: MovDpDp(DirectPage()); break;
            case 0xFB: MovY(DirectPageX()); break;
            case 0xFD: MovYA(); break;
            default:
                throw new NotImplementedException($"Opcode ${opcode:X2}");
        }
    }
}
