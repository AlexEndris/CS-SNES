namespace Hardware.Apu;

public interface IApuBus
{
    byte Read(ushort address);

    void Write(ushort address, byte value);

    void Wait();
}
