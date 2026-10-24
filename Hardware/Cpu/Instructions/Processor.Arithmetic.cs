namespace Hardware.Cpu;

public partial class Processor
{
    private void Adc(Operand operand)
    {
        if (Registers.M8)
        {
            byte value = ReadByte(operand);

            int sum = Registers.Decimal
                    ? BcdAdd(value)
                    : BitAdd(value);
            
            Registers.A = (ushort)((Registers.A & 0xFF00)| ((byte)sum));            
            
            SetZeroNegativeFlags((byte)Registers.A);
            Registers.Carry = (sum > 0xFF);
        }
        else
        {
            ushort value = ReadWord(operand);

            int sum = Registers.Decimal
                ? BcdAdd(value)
                : BitAdd(value);
            
            Registers.A = (ushort)sum;  
            
            SetZeroNegativeFlags(Registers.A);
            Registers.Carry = (sum > 0xFFFF);
        }
    }   
    
    private void Sbc(Operand operand)
    {
        if (Registers.M8)
        {
            byte value = (byte)~(ReadByte(operand));

            int sum = Registers.Decimal
                    ? BcdSub(value)
                    : BitAdd(value);
            
            Registers.A = (ushort)((Registers.A & 0xFF00)| ((byte)sum));            
            
            SetZeroNegativeFlags((byte)Registers.A);
            Registers.Carry = (sum > 0xFF);
        }
        else
        {
            ushort value = (ushort)~ReadWord(operand);

            int sum = Registers.Decimal
                ? BcdSub(value)
                : BitAdd(value);
            
            Registers.A = (ushort)sum;  
            
            SetZeroNegativeFlags(Registers.A);
            Registers.Carry = (sum > 0xFFFF);
        }
    }

    private int BitAdd(byte value)
    {
        byte a = (byte)Registers.A;
        int sum = (a + value + Registers.Carry.ToByte());
        Registers.Overflow = ((a ^ sum) & (value ^ sum) & 0x80) != 0;
        return sum;
    }

    private int BitAdd(ushort value)
    {
        ushort a = Registers.A;
        int sum = (a + value + Registers.Carry.ToByte());
        Registers.Overflow = ((a ^ sum) & (value ^ sum) & 0x8000) != 0;
        return sum;
    }

    private int BcdAdd(byte value)
    {
        byte a = (byte)Registers.A;
        int sum = (a & 0xF) + (value & 0xF) + Registers.Carry.ToByte();
        byte carry = 0;
        if (sum > 0x9)
        {
            sum += 6;
            carry = 0x10;
        }

        sum = (a & 0xF0) + (value & 0xF0) + carry + (sum & 0xF);
        Registers.Overflow = ((a ^ sum) & (value ^ sum) & 0x80) != 0;

        if (sum > 0x9F)
        {
            sum += 0x60;
        }

        return sum;
    }
    
    private int BcdAdd(ushort value)
    {
        ushort a = Registers.A;
        int sum = (a & 0xF) + (value & 0xF) + Registers.Carry.ToByte();
        ushort carry = 0;
        if (sum > 0x9)
        {
            sum += 0x6;
            carry = 0x10;
        }

        sum = (a & 0xF0) + (value & 0xF0) + carry + (sum & 0xF);
        carry = 0;
        if (sum > 0x9F)
        {
            sum += 0x60;
            carry = 0x100;
        }
        
        sum = (a & 0xF00) + (value & 0xF00) + carry + (sum & 0xFF);
        carry = 0;
        if (sum > 0x9FF)
        {
            sum += 0x600;
            carry = 0x1000;
        }

        sum = (a & 0xF000) + (value & 0xF000) + carry + (sum & 0xFFF);
        Registers.Overflow = ((a ^ sum) & (value ^ sum) & 0x8000) != 0;

        if (sum > 0x9FFF)
        {
            sum += 0x6000;
        }

        return sum;
    }
    
    private int BcdSub(byte value)
    {
        byte a = (byte)Registers.A;
        int sum = (a & 0xF) + (value & 0xF) + Registers.Carry.ToByte();
        byte carry = 0;
        if (sum <= 0xF)
            sum -= 6;
        if (sum > 0xF)
            carry = 0x10;
        
        sum = (a & 0xF0) + (value & 0xF0) + carry + (sum & 0xF);
        Registers.Overflow = ((a ^ sum) & (value ^ sum) & 0x80) != 0;

        if (sum <= 0xFF)
            sum -= 0x60;

        return sum;
    }
    
    private int BcdSub(ushort value)
    {
        ushort a = Registers.A;
        int sum = (a & 0xF) + (value & 0xF) + Registers.Carry.ToByte();
        ushort carry = 0;
        if (sum <= 0xF)
            sum -= 0x6;
        if (sum > 0xF)
            carry = 0x10;

        sum = (a & 0xF0) + (value & 0xF0) + carry + (sum & 0xF);
        carry = 0;
        if (sum <= 0xFF)
            sum -= 0x60;
        if (sum > 0xFF)
            carry = 0x100;
        
        sum = (a & 0xF00) + (value & 0xF00) + carry + (sum & 0xFF);
        carry = 0;
        if (sum <= 0xFFF)
            sum -= 0x600;
        if (sum > 0xFFF)
            carry = 0x1000;
        
        sum = (a & 0xF000) + (value & 0xF000) + carry + (sum & 0xFFF);
        Registers.Overflow = ((a ^ sum) & (value ^ sum) & 0x8000) != 0;

        if (sum <= 0xFFFF)
        {
            sum -= 0x6000;
        }

        return sum;
    }
}
