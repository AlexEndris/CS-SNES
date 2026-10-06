namespace Hardware.Cpu;

public partial class Processor
{
    private void Tax(Operand _)
    {
        if (Registers.X8)
        {
            Registers.X = (ushort)(Registers.X & 0xFF00 | Registers.A & 0xFF);
            SetZeroNegativeFlags((byte)Registers.X);
        }
        else
        {
            Registers.X = Registers.A;
            SetZeroNegativeFlags(Registers.X);
        }
    }
    
    private void Tay(Operand _)
    {
        if (Registers.X8)
        {
            Registers.Y = (ushort)(Registers.Y & 0xFF00 | Registers.A & 0xFF);
            SetZeroNegativeFlags((byte)Registers.Y);
        }
        else
        {
            Registers.Y = Registers.A;
            SetZeroNegativeFlags(Registers.Y);
        }
    }
    
    private void Tsx(Operand _)
    {
        if (Registers.X8)
        {
            Registers.X = (ushort)(Registers.X & 0xFF00 | Registers.S & 0xFF);
            SetZeroNegativeFlags((byte)Registers.X);
        }
        else
        {
            Registers.X = Registers.S;
            SetZeroNegativeFlags(Registers.X);
        }
    }
        
    private void Txa(Operand _)
    {
        if (Registers.M8)
        {
            Registers.A = (ushort)(Registers.A & 0xFF00 | Registers.X & 0xFF);
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            Registers.A = Registers.X;
            SetZeroNegativeFlags(Registers.A);
        }
    }

    private void Tya(Operand _)
    {
        if (Registers.M8)
        {
            Registers.A = (ushort)(Registers.A & 0xFF00 | Registers.Y & 0xFF);
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            Registers.A = Registers.Y;
            SetZeroNegativeFlags(Registers.A);
        }
    }
    
    private void Txy(Operand _)
    {
        if (Registers.X8)
        {
            Registers.Y = (ushort)(Registers.Y & 0xFF00 | Registers.X & 0xFF);
            SetZeroNegativeFlags((byte)Registers.Y);
        }
        else
        {
            Registers.Y = Registers.X;
            SetZeroNegativeFlags(Registers.Y);
        }
    }
    
    private void Tyx(Operand _)
    {
        if (Registers.X8)
        {
            Registers.X = (ushort)(Registers.X & 0xFF00 | Registers.Y & 0xFF);
            SetZeroNegativeFlags((byte)Registers.X);
        }
        else
        {
            Registers.X = Registers.Y;
            SetZeroNegativeFlags(Registers.X);
        }
    }

    private void Txs(Operand _)
    {
        Registers.S = Registers.X;
    }

    private void Tcd(Operand _)
    {
        Registers.D = Registers.A;
        SetZeroNegativeFlags(Registers.D);
    }

    private void Tcs(Operand _)
    {
        Registers.S = Registers.A;
    }
    
    private void Tdc(Operand _)
    {
        Registers.A = Registers.D;
        SetZeroNegativeFlags(Registers.A);
    }
    
    private void Tsc(Operand _)
    {
        Registers.A = Registers.S;
        SetZeroNegativeFlags(Registers.A);
    }

    private void Xba(Operand _)
    {
        byte a = (byte)Registers.A;
        byte b = (byte)(Registers.A >> 8);

        Registers.A = (ushort)((a << 8) | b);
        
        SetZeroNegativeFlags((byte)Registers.A);
        cpuBus.Idle();
    }
}
