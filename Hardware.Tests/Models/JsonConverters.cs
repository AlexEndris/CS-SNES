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
        uint? address = reader.TokenType == JsonTokenType.Null ? null : reader.GetUInt32();
        reader.Read();
        byte? value = reader.TokenType == JsonTokenType.Null ? null : reader.GetByte();
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

public sealed class BoolConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) =>
        reader.TokenType == JsonTokenType.Number ? reader.GetInt32() != 0 : reader.GetBoolean();

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value ? 1 : 0);
}
