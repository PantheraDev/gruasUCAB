/*
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class UserTypeValueObjectConverter : ValueConverter<UserTypeValueObject, int>
{
    public UserTypeValueObjectConverter() : base(
        v => (int)v.Value,
        v => UserTypeValueObject.Create((UsersType)v)
    ) { }
}*/