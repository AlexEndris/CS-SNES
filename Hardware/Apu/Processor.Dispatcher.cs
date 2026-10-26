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
            case 0xBF: MovAIndirect(IndirectInc()); break;
            case 0xCD: MovX(Immediate()); break;
            case 0xDD: MovAY(); break;
            case 0xE4: MovA(DirectPage()); break;
            case 0xE5: MovA(Absolute()); break;
            case 0xE6: MovA(Indirect()); break;
            case 0xE8: MovA(Immediate()); break;
            case 0xE9: MovX(Absolute()); break;
            case 0xEB: MovY(DirectPage()); break;
            case 0xEC: MovY(Absolute()); break;
            case 0xF4: MovA(DirectPageX()); break;
            case 0xF5: MovA(AbsoluteX()); break;
            case 0xF6: MovA(AbsoluteY()); break;
            case 0xF8: MovX(DirectPage()); break;
            case 0xF9: MovX(DirectPageY()); break;
            case 0xFB: MovY(DirectPageX()); break;
            case 0xFD: MovYA(); break;
            default:
                throw new NotImplementedException($"Opcode ${opcode:X2}");
        }
    }
}
