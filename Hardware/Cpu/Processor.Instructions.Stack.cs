namespace Hardware.Cpu;

public partial class Processor
{
    private void Pea(Operand operand)
    {
        StackOperation(operand);
    }

    private void Pei(Operand operand)
    {
        StackOperation(operand);
    }

    private void Per(Operand operand)
    {
        ushort value = ReadWord(operand);
        cpuBus.Idle();
        Push((ushort)(Registers.PC + value));
    }

    private void Pha(Operand _)
    {
        if (Registers.M8)
        {
            Push((byte) Registers.A);
        }
        else
        {
            Push(Registers.A);
        }
    }
    
    private void Phx(Operand _)
    {
        if (Registers.X8)
        {
            Push((byte) Registers.X);
        }
        else
        {
            Push(Registers.X);
        }
    }
    
    private void Phy(Operand _)
    {
        if (Registers.X8)
        {
            Push((byte) Registers.Y);
        }
        else
        {
            Push(Registers.Y);
        }
    }
    
    private void Pla(Operand _)
    {
        cpuBus.Idle();
        if (Registers.M8)
        {
            Registers.A = (ushort)((Registers.A & 0xFF00) | PullByte());
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            Registers.A = PullWord();
            SetZeroNegativeFlags(Registers.A);
        }
    }
    
    private void Plx(Operand _)
    {
        cpuBus.Idle();
        if (Registers.X8)
        {
            Registers.X = (ushort)((Registers.X & 0xFF00) | PullByte());
            SetZeroNegativeFlags((byte)Registers.X);
        }
        else
        {
            Registers.X = PullWord();
            SetZeroNegativeFlags(Registers.X);
        }
    }
    
    private void Ply(Operand _)
    {
        cpuBus.Idle();
        if (Registers.X8)
        {
            Registers.Y = (ushort)((Registers.Y & 0xFF00) | PullByte());
            SetZeroNegativeFlags((byte)Registers.Y);
        }
        else
        {
            Registers.Y = PullWord();
            SetZeroNegativeFlags(Registers.Y);
        }
    }
    
    private void Push(ushort value)
    {
        var pointer = Registers.S;
        cpuBus.Write(pointer--, (byte)(value >> 8));
        cpuBus.Write(pointer--, (byte)value);
        Registers.S = pointer;
    }
    
    private void Push(byte value)
    {
        cpuBus.Write(Registers.S--, value);
    }

    private ushort PullWord()
    {
        var pointer = Registers.S;
        pointer++;
        byte low = cpuBus.Read(pointer);
        pointer++;
        byte high = cpuBus.Read(pointer);
        Registers.S = pointer;
        
        return (ushort)(high << 8 | low);
    }
    
    private byte PullByte()
    {
        Registers.S++;
        return cpuBus.Read(Registers.S);
    }
    
    private void StackOperation(Operand operand)
    {
        ushort value = ReadWord(operand);
        Push(value);
    }
}
