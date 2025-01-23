namespace UserMs.Domain.Entities
{
    public class LicenseId : ValueObject
    {
        public Guid Value { get; }

        private LicenseId(Guid value)
        {
            Value = value;
        }

        public static LicenseId Create()
        {
            return new LicenseId(Guid.NewGuid());
        }

        public static LicenseId Create(Guid value)
        {
            return new LicenseId(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}