namespace Hardware.Cpu;

using static Operand;

public partial class Processor
{
    private Operand Implied()
    {
        cpuBus.Idle();
        return Nothing;
    }

    private Operand Immediate8()
    {
        uint address = Registers.ProgramAddress;
        Registers.PC++;
        return WithinBank(address);
    }
    
    private Operand Immediate16()
    {
        uint address = Registers.ProgramAddress;
        Registers.PC++;
        Registers.PC++;
        return WithinBank(address);
    }
    
    private Operand ImmediateM()
    {
        uint address = Registers.ProgramAddress;
        Registers.PC += (ushort)(Registers.M8
            ? 1
            : 2);
        return WithinBank(address);
    }
    
    private Operand ImmediateX()
    {
        uint address = Registers.ProgramAddress;
        Registers.PC += (ushort)(Registers.X8
            ? 1
            : 2);
        return WithinBank(address);
    }

    private Operand Absolute()
    {
        ushort readAddress = FetchWord();
        uint address = (uint)((Registers.Dbr << 16) | readAddress);
        
        return CrossBank(address);
    }
    
    private Operand AbsoluteX(bool isWrite = false)
    {
        ushort readAddress = FetchWord();
        uint baseAddress = (uint)((Registers.Dbr << 16) | readAddress);
        uint address = (baseAddress + Registers.X) & 0xFFFFFF;
        
        if (baseAddress >> 8 != address >> 8
            || !Registers.X8
            || isWrite)
            cpuBus.Idle();
        
        return CrossBank(address);
    }
    
    private Operand AbsoluteY(bool isWrite = false)
    {
        ushort readAddress = FetchWord();
        uint baseAddress = (uint)((Registers.Dbr << 16) | readAddress);
        uint address = (baseAddress + Registers.Y) & 0xFFFFFF;
        
        if (baseAddress >> 8 != address >> 8
            || !Registers.X8
            || isWrite)
            cpuBus.Idle();
        
        return CrossBank(address);
    }

    private Operand AbsoluteLong()
    {
        uint address = FetchLong();
        return CrossBank(address);
    }
    
    private Operand AbsoluteLongX()
    {
        uint baseAddress = FetchLong();
        uint address = (baseAddress + Registers.X) & 0xFFFFFF;
        
        return CrossBank(address);
    }

    private Operand Direct()
    {
        byte lowByte = FetchByte();
        uint address = (uint)((Registers.D + lowByte) & 0x00FFFF);

        if (Registers.DL > 0)
            cpuBus.Idle();
        
        return WithinBank(address);
    }
    
    private Operand DirectX()
    {
        byte lowByte = FetchByte();
        uint directAddress = (uint)((Registers.D + lowByte) & 0x00FFFF);

        if (Registers.DL > 0)
            cpuBus.Idle();

        cpuBus.Idle();

        uint address = (directAddress + Registers.X) & 0x00FFFF;

        if (Registers is
            {
                EmulationMode: true,
                DL: 0
            })
        {
            address = (uint)((Registers.DH << 8) | ((lowByte + Registers.X) & 0xFF));
        }

        return WithinBank(address);
    }
    
    private Operand DirectY()
    {
        byte lowByte = FetchByte();
        uint directAddress = (uint)((Registers.D + lowByte) & 0x00FFFF);

        if (Registers.DL > 0)
            cpuBus.Idle();

        cpuBus.Idle();

        uint address = (directAddress + Registers.Y) & 0x00FFFF;

        if (Registers is
            {
                EmulationMode: true,
                DL: 0
            })
        {
            address = (uint)(Registers.DH << 8 | ((lowByte + Registers.Y) & 0xFF));
        }

        return WithinBank(address);
    }

    private Operand StackRelative()
    {
        byte offset = FetchByte();

        uint address = (uint)((Registers.S + offset) & 0x00FFFF);

        cpuBus.Idle();
        
        return WithinBank(address);
    }
    
    private Operand Relative8()
    {
        return Immediate8();
    }
    
    private Operand Relative16()
    {
        return Immediate16();
    }
}
