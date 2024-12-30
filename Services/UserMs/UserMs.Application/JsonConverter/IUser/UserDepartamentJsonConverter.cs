using System.Text.Json;
using System.Text.Json.Serialization;
using UserMs.Domain.Entities;

    public class UserDepartamentJsonConverter : JsonConverter<UserDepartament>
    {
        public override UserDepartament Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetGuid();

            return UserDepartament.Create(value);
        }

        public override void Write(Utf8JsonWriter writer, UserDepartament value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }