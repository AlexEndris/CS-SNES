namespace Hardware.Cpu;

public partial class Processor
{
    private void Mvn()
    {
        ExecuteMove(1);
    }

    private void Mvp()
    {
        ExecuteMove(-1);
    }

    private void ExecuteMove(int step)
    {
        byte destinationBank = FetchByte();
        byte sourceBank = FetchByte();

        ushort sourceAddress = Registers.X;
        ushort destinationAddress = Registers.Y;

        uint source = (uint)(sourceBank << 16 | sourceAddress);
        uint destination = (uint)(destinationBank << 16 | destinationAddress);

        Registers.Dbr = sourceBank;
        byte data = ReadByte(Operand.WithinBank(source));
        Registers.Dbr = destinationBank;
        WriteByte(Operand.WithinBank(destination), data);

        cpuBus.Idle();
        cpuBus.Idle();
        
        Registers.X = (ushort)(Registers.X + step);
        Registers.Y = (ushort)(Registers.Y + step);
        
        Registers.A--;
        if (Registers.A == 0xFFFF)
        {
            return;
        }

        Registers.PC -= 3;
    }
}
