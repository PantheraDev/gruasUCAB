namespace UserMs.Domain.Entities
{
public class LicenseNumber : ValueObject
{
    public string Value { get; }

    public LicenseNumber(string value)
    {
        Value = value;
    }

    public static LicenseNumber Create(string value)
    {
        return new LicenseNumber(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
}