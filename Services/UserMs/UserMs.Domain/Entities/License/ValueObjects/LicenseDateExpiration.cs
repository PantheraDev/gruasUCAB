namespace UserMs.Domain.Entities
{
    public class LicenseDateExpiration : ValueObject
    {
        public DateTime Value { get; }

        private LicenseDateExpiration(DateTime value)
        {
            Value = value;
        }

        public static LicenseDateExpiration Create(DateTime value)
        {
            return new LicenseDateExpiration(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}