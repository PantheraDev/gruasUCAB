using System.Text.Json;
using System.Text.Json.Serialization;
using UserMs.Domain.Entities;

    public class UserIdJsonConverter : JsonConverter<UserId>
    {
        public override UserId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetGuid();

            return UserId.Create(value);
        }

        public override void Write(Utf8JsonWriter writer, UserId value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }