namespace Hardware.Tests.Models;

using System.Text.Json.Serialization;

[JsonConverter(typeof(CycleConverter))]
public class Cycle
{
    public uint? Address { get; set; }

    public byte? Value { get; set; }

    public string? Outputs { get; set; }
}
