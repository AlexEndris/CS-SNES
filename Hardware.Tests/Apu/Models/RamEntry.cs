namespace Hardware.Tests.Apu.Models;

using System.Text.Json.Serialization;

[JsonConverter(typeof(RamEntryConverters))]
public class RamEntry
{
    public ushort Address { get; set; }

    public byte Value { get; set; }
}
