namespace Hardware.Cpu;

public partial class Processor
{
    private void Bra(Operand operand)
    {
        sbyte offset = (sbyte)ReadByte(operand);
        TakeBranch(offset);
    }

    private void Bcc(Operand operand)
    {
        sbyte offset = (sbyte)ReadByte(operand);

        if (Registers.Carry)
            return;
        
        TakeBranch(offset);
    }
    
    private void Bcs(Operand operand)
    {
        sbyte offset = (sbyte)ReadByte(operand);

        if (!Registers.Carry)
            return;
        
        TakeBranch(offset);
    }
    
    private void Bne(Operand operand)
    {
        sbyte offset = (sbyte)ReadByte(operand);

        if (Registers.Zero)
            return;
        
        TakeBranch(offset);
    }
    
    private void Beq(Operand operand)
    {
        sbyte offset = (sbyte)ReadByte(operand);

        if (!Registers.Zero)
            return;
        
        TakeBranch(offset);
    }
    
    private void Bpl(Operand operand)
    {
        sbyte offset = (sbyte)ReadByte(operand);

        if (Registers.Negative)
            return;
        
        TakeBranch(offset);
    }
    
    private void Bmi(Operand operand)
    {
        sbyte offset = (sbyte)ReadByte(operand);

        if (!Registers.Negative)
            return;
        
        TakeBranch(offset);
    }
    
    private void Bvc(Operand operand)
    {
        sbyte offset = (sbyte)ReadByte(operand);

        if (Registers.Overflow)
            return;
        
        TakeBranch(offset);
    }
    
    private void Bvs(Operand operand)
    {
        sbyte offset = (sbyte)ReadByte(operand);

        if (!Registers.Overflow)
            return;
        
        TakeBranch(offset);
    }
    
    private void Brl(Operand operand)
    {
        short offset = (short)ReadWord(operand);
        
        cpuBus.Idle();
        ushort result = (ushort)(Registers.PC + offset);

        Registers.PC = result;
    }
    
    private void TakeBranch(sbyte offset)
    {
        cpuBus.Idle();
        ushort result = (ushort)(Registers.PC + offset);

        if (Registers.EmulationMode
            && (Registers.PC & 0xFF00) != (result & 0xFF00))
        {
            cpuBus.Idle();
        }
        
        Registers.PC = result;
    }    
}
