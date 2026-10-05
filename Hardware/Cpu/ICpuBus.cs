namespace Hardware.Cpu;

public interface ICpuBus
{
       byte Read(uint address);

       void Write(uint address, byte value);

       void Idle();
}
