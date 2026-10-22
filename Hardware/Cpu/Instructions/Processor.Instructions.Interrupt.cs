namespace Hardware.Cpu;

using static Operand;

public partial class Processor
{
    private void Brk()
    {
        EnterInterrupt(0xFFE6, 0xFFFE);
    }

    private void Cop()
    {
        EnterInterrupt(0xFFE4,0xFFF4);
    }

    private void Rti()
    {
        cpuBus.Idle();
        cpuBus.Idle();
        if (Registers.EmulationMode)
        {
            Registers.P = PullByteWrapping();
            Registers.PC = PullWordWrapping();
        }
        else
        {
            Registers.P = PullByte();
            Registers.PC = PullWord();
            Registers.Pbr = PullByte();
        }
    }
    
    private void EnterInterrupt(ushort nativeVector, ushort emulationVector)
    {
        FetchByte();
        if (Registers.EmulationMode)
        {
            PushWrapping(Registers.PC);
            Push(Registers.P);
            Registers.Pbr = 0;
            Registers.PC = ReadWord(WithinBank(emulationVector));
        }
        else
        {
            Push(Registers.Pbr);
            Push(Registers.PC);
            Push(Registers.P);
            Registers.Pbr = 0;
            Registers.PC = ReadWord(WithinBank(nativeVector));
        }
        Registers.Decimal = false;
        Registers.IrqDisable = true;
    }
}
