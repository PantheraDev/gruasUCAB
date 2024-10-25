/*public class DriverAvailable : ValueObject
{
    public bool IsAvailable { get; }

    public DriverAvailable(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }

    public static DriverAvailable Create(bool value)
    {
        return new DriverAvailable(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IsAvailable;
    }
}*/