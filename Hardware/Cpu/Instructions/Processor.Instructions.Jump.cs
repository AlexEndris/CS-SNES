namespace Hardware.Cpu;

using static Operand;

public partial class Processor
{
    private void Jmp(Operand operand)
    {
        Registers.PC = (ushort)operand.Address;
    }
    
    private void Jml(Operand operand)
    {
        Registers.Pbr = (byte)(operand.Address >> 16);
        Registers.PC = (ushort)(operand.Address);
    }

    private void Jsl(Operand operand)
    {
        ushort stackPointer = Registers.S;
        
        cpuBus.Write(stackPointer--, Registers.Pbr);
        cpuBus.Idle();
        Registers.Pbr = FetchByte();

        ushort pcAddress = (ushort)(Registers.PC - 1);
        cpuBus.Write(stackPointer--, (byte)(pcAddress >> 8));
        cpuBus.Write(stackPointer--, (byte)pcAddress);

        Registers.S = stackPointer;
        Registers.PC = (ushort)operand.Address;
    }

    private void Jsr(Operand operand)
    {
        cpuBus.Idle();
        ushort pcAddress = (ushort)(Registers.PC - 1);
        cpuBus.Write(Registers.S--, (byte)(pcAddress >> 8));
        cpuBus.Write(Registers.S--, (byte)pcAddress);
        
        Registers.PC = (ushort)operand.Address;
    }
    
    private void JsrIndirect()
    {
        byte lowPointerByte = FetchByte();
        
        ushort pcAddress = Registers.PC;
        cpuBus.Write(Registers.S--, (byte)(pcAddress >> 8));
        cpuBus.Write(Registers.S--, (byte)pcAddress);
        
        ushort basePointer = (ushort)(FetchByte() << 8 | lowPointerByte);
        ushort pointer = (ushort)((basePointer + Registers.X) & 0xFFFF);
        uint pointerAddress = (uint)((Registers.Pbr << 16) | pointer);
        
        cpuBus.Idle();
        
        ushort address = ReadWord(WithinBank(pointerAddress));

        Registers.PC = address;
    }

    private void Rtl(Operand _)
    {
        ushort stackPointer = Registers.S;

        cpuBus.Idle();
        stackPointer++;
        byte lowByte = cpuBus.Read(stackPointer);
        stackPointer++;
        byte highByte = cpuBus.Read(stackPointer);

        Registers.PC = (ushort)((highByte << 8) | lowByte);
        Registers.PC++;
        
        stackPointer++;
        Registers.Pbr = cpuBus.Read(stackPointer);

        Registers.S = stackPointer;
    }

    private void Rts(Operand _)
    {
        cpuBus.Idle();
        Registers.S++;
        byte lowByte = cpuBus.Read(Registers.S);
        Registers.S++;
        byte highByte = cpuBus.Read(Registers.S);
        
        cpuBus.Idle();
        
        Registers.PC = (ushort)((highByte << 8) | lowByte);
        Registers.PC++;
    }
}
