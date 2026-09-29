namespace Hardware;

public class Cpu(IBus Bus)
{ 
    public CpuRegisters Registers { get; protected set; }
    
    public byte Cycle { get; private set; }

    public void Tick()
    {
        
    }
}
