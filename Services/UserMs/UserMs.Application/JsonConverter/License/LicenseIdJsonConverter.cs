using System.Text.Json;
using System.Text.Json.Serialization;
using UserMs.Domain.Entities;

    public class LicenseIdJsonConverter : JsonConverter<LicenseId>
    {
        public override LicenseId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetGuid();

            return LicenseId.Create(value);
        }

        public override void Write(Utf8JsonWriter writer, LicenseId value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }