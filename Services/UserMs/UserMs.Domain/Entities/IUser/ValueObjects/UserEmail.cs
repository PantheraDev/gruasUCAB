namespace UserMs.Domain.Entities
{
    public class UserEmail : ValueObject
    {
        public string Value { get; }

        private UserEmail(string value)
        {
            Value = value;
        }

        public static UserEmail Create(string value)
        {
            return new UserEmail(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}