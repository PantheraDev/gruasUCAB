namespace UserMs.Domain.Entities
{
    public class LicenseDelete : ValueObject
    {
        public bool Value { get; }

        private LicenseDelete(bool value = false)
        {
            Value = value;
        }

        public static LicenseDelete Create(bool value)
        {
            return new LicenseDelete(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}