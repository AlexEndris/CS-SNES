namespace Hardware.Cpu;

public partial class Processor
{
    private void Inc(Operand operand)
    {
        if (Registers.M8)
        {
            byte data = ReadByte(operand);
            data += 1;
            WriteByte(operand, data);
            SetZeroNegativeFlags(data);
        }
        else
        {
            ushort data = ReadWord(operand);
            data += 1;
            WriteWord(operand, data);
            SetZeroNegativeFlags(data);
        }
    }
    
    private void IncA(Operand _)
    {
        if (Registers.M8)
        {
            Registers.A = (ushort)((Registers.A & 0xFF00) | (Registers.A + 1 & 0xFF));
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            Registers.A += 1;
            SetZeroNegativeFlags(Registers.A);
        }
    }    
    
    private void Inx(Operand _)
    {
        if (Registers.X8)
        {
            Registers.X = (ushort)((Registers.X & 0xFF00) | (Registers.X + 1 & 0xFF));
            SetZeroNegativeFlags((byte)Registers.X);
        }
        else
        {
            Registers.X += 1;
            SetZeroNegativeFlags(Registers.X);
        }
    }    
    
    private void Iny(Operand _)
    {
        if (Registers.X8)
        {
            Registers.Y = (ushort)((Registers.Y & 0xFF00) | (Registers.Y + 1 & 0xFF));
            SetZeroNegativeFlags((byte)Registers.Y);
        }
        else
        {
            Registers.Y += 1;
            SetZeroNegativeFlags(Registers.Y);
        }
    }    
    
    private void DecA(Operand _)
    {
        if (Registers.M8)
        {
            Registers.A = (ushort)((Registers.A & 0xFF00) | (Registers.A - 1 & 0xFF));
            SetZeroNegativeFlags((byte)Registers.A);
        }
        else
        {
            Registers.A -= 1;
            SetZeroNegativeFlags(Registers.A);
        }
    }   
    
    private void Dex(Operand _)
    {
        if (Registers.X8)
        {
            Registers.X = (ushort)((Registers.X & 0xFF00) | (Registers.X - 1 & 0xFF));
            SetZeroNegativeFlags((byte)Registers.X);
        }
        else
        {
            Registers.X -= 1;
            SetZeroNegativeFlags(Registers.X);
        }
    } 
    
    private void Dey(Operand _)
    {
        if (Registers.X8)
        {
            Registers.Y = (ushort)((Registers.Y & 0xFF00) | (Registers.Y - 1 & 0xFF));
            SetZeroNegativeFlags((byte)Registers.Y);
        }
        else
        {
            Registers.Y -= 1;
            SetZeroNegativeFlags(Registers.Y);
        }
    } 
}
