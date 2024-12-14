using System.Text.Json;
using System.Text.Json.Serialization;
using UserMs.Domain.Entities;

public class LicenseNumberJsonConverter : JsonConverter<LicenseNumber>
{
    public override LicenseNumber Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected a string.");
        }

        var number = reader.GetString();
        return LicenseNumber.Create(number);
    }

    public override void Write(Utf8JsonWriter writer, LicenseNumber value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}