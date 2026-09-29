namespace Hardware.Tests.Models;

using System.Text.Json.Serialization;

[JsonConverter(typeof(RamEntryConverters))]
public class RamEntry
{
    public uint Address { get; set; }

    public byte Value { get; set; }
}
