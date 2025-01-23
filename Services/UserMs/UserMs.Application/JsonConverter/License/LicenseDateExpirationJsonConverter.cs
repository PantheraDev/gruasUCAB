using System.Text.Json;
using System.Text.Json.Serialization;
using UserMs.Domain.Entities;

    public class LicenseDateExpirationJsonConverter : JsonConverter<LicenseDateExpiration>
    {
        public override LicenseDateExpiration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException("Expected a string.");
            }

            var dateString = reader.GetString();
            if (!DateTime.TryParseExact(dateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                throw new JsonException($"Unable to parse date: {dateString}");
            }

            return LicenseDateExpiration.Create(result);
        }

        public override void Write(Utf8JsonWriter writer, LicenseDateExpiration value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd"));
        }
    }
