namespace Hardware;

public interface IBus
{
       byte ReadByte(uint address);
       void WriteByte(uint address, byte value);      
}
