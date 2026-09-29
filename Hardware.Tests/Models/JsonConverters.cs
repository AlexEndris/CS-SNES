namespace Hardware.Tests.Models;

using System.Text.Json;
using System.Text.Json.Serialization;

public class RamEntryConverters : JsonConverter<RamEntry>
{
    public override RamEntry? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected [address, value] array.");

        reader.Read();
        var address = reader.GetUInt32();
        reader.Read();
        var value = reader.GetByte();
        reader.Read();
        if (reader.TokenType != JsonTokenType.EndArray)
            throw new JsonException("Expected end of [address, value] array.");

        return new RamEntry { Address = address, Value = value };
    }

    public override void Write(Utf8JsonWriter writer, RamEntry value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

public class CycleConverter : JsonConverter<Cycle>
{
    public override Cycle? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected [address, value, outputs] array.");

        reader.Read();
        var address = reader.GetUInt32();
        reader.Read();
        var value = reader.GetByte();
        reader.Read();
        var outputs = reader.GetString();
        reader.Read();
        if (reader.TokenType != JsonTokenType.EndArray)
            throw new JsonException("Expected end of [address, value, outputs] array.");

        return new Cycle { Address = address, Value = value, Outputs = outputs };
    }

    public override void Write(Utf8JsonWriter writer, Cycle value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
