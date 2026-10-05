namespace Hardware.Tests.Models;

public struct BusLogEntry
{
    public uint? Address { get; set; }
    public byte? Value { get; set; }
    public string Kind { get; set; }
}
