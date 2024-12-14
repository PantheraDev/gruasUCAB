/*
public class UserTypeValueObject : ValueObject
{
    public UsersType Value { get; }

    private UserTypeValueObject(UsersType value)
    {
        Value = value;
    }

    public static UserTypeValueObject Create(UsersType value)
    {
        return new UserTypeValueObject(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

        public static UsersType FromString(string value)
    {
        switch (value.ToLower())
        {
            case "administrador":
                return UsersType.Administrador;
            case "operador":
                return UsersType.Operador;
            case "proveedor":
                return UsersType.Proveedor;
            default:
                throw new ArgumentException("Invalid user type");
        }
    }
}
*/