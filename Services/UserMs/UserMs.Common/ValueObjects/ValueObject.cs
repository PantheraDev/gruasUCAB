public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object?  obj)
    {
        if (obj == null   
 || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()   

    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) => current   
 * 31 + obj.GetHashCode());
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null))
        {
            return ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator   
 !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }
}