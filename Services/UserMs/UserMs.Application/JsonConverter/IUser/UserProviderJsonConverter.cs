using System.Text.Json;
using System.Text.Json.Serialization;
using UserMs.Domain.Entities;

    public class UserProviderJsonConverter : JsonConverter<UserProvider>
    {
        public override UserProvider Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetGuid();

            return UserProvider.Create(value);
        }

        public override void Write(Utf8JsonWriter writer, UserProvider value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }