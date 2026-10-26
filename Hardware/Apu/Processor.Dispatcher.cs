namespace Hardware.Apu;

public partial class Processor
{
    private void Execute(byte opcode)
    {
        switch (opcode)
        {
            case 0x00: Nop(); break; 
            case 0x7D: MovAX(); break;
            case 0xDD: MovAY(); break;
            case 0xE8: MovA(Immediate()); break;
            default:
                throw new NotImplementedException($"Opcode ${opcode:X2}");
        }
    }
}
