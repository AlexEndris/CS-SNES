namespace Hardware.Cpu;

using static Operand;

public partial class Processor
{
    private Operand AbsoluteIndirect()
    {
        ushort pointer = FetchWord();
        
        uint address = (uint)((Registers.Pbr << 16) | ReadWord(WithinBank(pointer)));

        return CrossBank(address);
    }
    
    private Operand AbsoluteIndirectLong()
    {
        ushort pointer = FetchWord();

        uint address = ReadLong(WithinBank(pointer));
        
        return CrossBank(address);
    }
    
    private Operand AbsoluteIndexedIndirect()
    {
        ushort basePointer = FetchWord();
        ushort pointer = (ushort)((basePointer + Registers.X) & 0xFFFF);

        uint pointerAddress = (uint)((Registers.Pbr << 16) | pointer);
        cpuBus.Idle();
        ushort address = ReadWord(WithinBank(pointerAddress));

        return CrossBank(address);
    }
    
    private Operand DirectIndirect()
    {
        byte lowByte = FetchByte();
        uint directAddress = (uint)((Registers.D + lowByte) & 0x00FFFF);

        if ((Registers.DL) > 0)
            cpuBus.Idle();

        var pointerOperand = Registers is { EmulationMode: true, DL: 0 } ? WithinPage(directAddress) : WithinBank(directAddress);
        
        uint address = (uint)((Registers.Dbr << 16) | ReadWord(pointerOperand));

        return CrossBank(address);
    }
    
    private Operand DirectIndirectLong()
    {
        byte lowByte = FetchByte();
        uint directAddress = (uint)((Registers.D + lowByte) & 0x00FFFF);

        if ((Registers.DL) > 0)
            cpuBus.Idle();

        uint address = ReadLong(WithinBank(directAddress));

        return CrossBank(address);
    }
    
    private Operand DirectIndirectY(bool isWrite = false)
    {
        byte lowByte = FetchByte();
        uint directAddress = (uint)((Registers.D + lowByte) & 0x00FFFF);

        if ((Registers.DL) > 0)
            cpuBus.Idle();

        var pointerOperand = Registers is { EmulationMode: true, DL: 0 } ? WithinPage(directAddress) : WithinBank(directAddress);
        
        uint baseAddress = (uint)((Registers.Dbr << 16) | ReadWord(pointerOperand));
        uint address = (baseAddress + Registers.Y) & 0xFFFFFF;
        
        if (baseAddress >> 8 != address >> 8
            || !Registers.X8
            || isWrite)
            cpuBus.Idle();

        return CrossBank(address);
    }
    
    private Operand DirectIndirectLongY()
    {
        byte lowByte = FetchByte();
        uint directAddress = (uint)((Registers.D + lowByte) & 0x00FFFF);

        if ((Registers.DL) > 0)
            cpuBus.Idle();

        uint baseAddress = ReadLong(WithinBank(directAddress));
        uint address = (baseAddress + Registers.Y) & 0xFFFFFF;
        
        return CrossBank(address);
    }

    private Operand DirectIndexedIndirect()
    {
        byte lowByte = FetchByte();
        uint directAddress = (uint)((Registers.D + lowByte + Registers.X) & 0x00FFFF);

        if (Registers is
            {
                EmulationMode: true,
                DL: 0
            })
        {
            directAddress = ((uint)((Registers.DH << 8) | ((lowByte + Registers.X) & 0xFF)));
        }
        
        if ((Registers.DL) > 0)
            cpuBus.Idle();
        
        cpuBus.Idle();
        
        uint address = (uint)((Registers.Dbr << 16) | ReadWord(WithinBank(directAddress)));

        return CrossBank(address);
    }
    
    private Operand StackRelativeIndirectY()
    {
        byte offset = FetchByte();

        uint pointerAddress = (uint)((Registers.S + offset) & 0x00FFFF);

        cpuBus.Idle();
        
        uint baseAddress = (uint)((Registers.Dbr << 16) | ReadWord(WithinBank(pointerAddress)));
        
        cpuBus.Idle();
        
        uint address = (baseAddress + Registers.Y) & 0xFFFFFF;
         
        return CrossBank(address);
    }
}
