using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserMs.Domain.Entities;

public class UserEmailValueConverter : ValueConverter<UserEmail, string>
{
    public UserEmailValueConverter() : base(
        v => v.Value, // Convierte UserEmail a string
        v => UserEmail.Create(v) // Convierte string a UserEmail
    ) { }
}

public class UserIdValueConverter : ValueConverter<UserId, Guid>
{
    public UserIdValueConverter() : base(
        v => v.Value, // Convierte UserId a Guid
        v => UserId.Create(v) // Convierte Guid a UserId
    ) { }
}

public class UserPasswordValueConverter : ValueConverter<UserPassword, string>
{
    public UserPasswordValueConverter() : base(
        v => v.Value, // Convierte UserPassword a string
        v => UserPassword.Create(v) // Convierte string a UserPassword
    ) { }
}

public class UserDeleteConverter : ValueConverter<UserDelete, bool>
{
    public UserDeleteConverter() : base(
        u => u.Value,
        b => UserDelete.Create(b)
    ) { }
}

public class UserProviderValueConverter : ValueConverter<UserProvider, Guid>
{
    public UserProviderValueConverter() : base(
        v => v.Value, // Convierte UserId a Guid
        v => UserProvider.Create(v) // Convierte Guid a UserId
    ) { }
}

public class UserDepartamentValueConverter : ValueConverter<UserDepartament, Guid>
{
    public UserDepartamentValueConverter() : base(
        v => v.Value, // Convierte UserId a Guid
        v => UserDepartament.Create(v) // Convierte Guid a UserId
    ) { }
}