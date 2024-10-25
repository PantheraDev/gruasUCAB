namespace UserMs.Domain.Entities
{
    public class UserPassword : ValueObject
    {
        public string Value { get; }

        private UserPassword(string value)
        {
            Value = value;
        }

        public static UserPassword Create(string value)
        {
            return new UserPassword(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}