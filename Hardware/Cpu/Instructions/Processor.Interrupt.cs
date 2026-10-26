namespace Hardware.Cpu;

using static Operand;

public partial class Processor
{
    private void Brk()
    {
        FetchByte();
        EnterInterrupt(0xFFE6, 0xFFFE);
    }

    private void Cop()
    {
        FetchByte();
        EnterInterrupt(0xFFE4,0xFFF4);
    }

    private void Rti()
    {
        Idle();
        Idle();
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

    private void Wai()
    {
        Idle();
        Idle();
        Idle();
        State = RunState.Waiting;
    }

    private void Stp()
    {
        Idle();
        Idle();
        Idle();
        State = RunState.Stopped;
    }
    
    private void EnterInterrupt(ushort nativeVector, ushort emulationVector, bool hardwareInterrupt = false)
    {
        if (Registers.EmulationMode)
        {
            PushWrapping(Registers.PC);
            if (hardwareInterrupt)
            {
                Push((byte)(Registers.P & ~(byte)Flags.XSizeBreak));
            }
            else
            {
                Push(Registers.P);
            }
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
