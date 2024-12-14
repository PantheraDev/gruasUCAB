using System.Text.Json;
using System.Text.Json.Serialization;
using UserMs.Domain.Entities;
using UserMs.Infrastructure.Exceptions;

public class UserPasswordJsonConverter : JsonConverter<UserPassword>
{
    public override UserPassword Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new NullAtributeException("UserPassword is a string and can't be null");
        }

        var value = reader.GetString();

        if (string.IsNullOrEmpty(value)) 
        {
            throw new NullAtributeException("UserPassword is a string and can't be null");
        }

        return UserPassword.Create(value);
    }

    public override void Write(Utf8JsonWriter writer, UserPassword value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}