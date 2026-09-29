namespace Hardware.Tests.Models;

using AwesomeAssertions;

public class TestBus : IBus
{
    public Dictionary<uint, byte> Ram = new();

    public void SetRam(RamEntry[] entries)
    {
        foreach (RamEntry ramEntry in entries)
        {
            Ram[ramEntry.Address] = ramEntry.Value;
        }
    }

    public void AssertRam(string name, RamEntry[] entries)
    {
        foreach (RamEntry ramEntry in entries)
        {
            Ram.Should().ContainKey(ramEntry.Address, name);
            Ram.Should().Match(e => Ram[ramEntry.Address] == ramEntry.Value, name);
        }

        foreach (var ramEntry in Ram)
        {
            entries.Should().Contain(e => e.Address == ramEntry.Key, name);
        }
    }
    
    public byte ReadByte(uint address)
    {
        return Ram[address];
    }

    public void WriteByte(uint address, byte value)
    {
        Ram[address] = value;
    }
}
