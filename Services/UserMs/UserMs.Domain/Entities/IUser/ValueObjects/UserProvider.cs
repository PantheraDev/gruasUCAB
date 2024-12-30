    namespace UserMs.Domain.Entities
    {
        public class UserProvider : ValueObject
        {
            public Guid Value { get; }

            private UserProvider(Guid value)
            {
                Value = value;
            }

            public static UserProvider Create(Guid value)
            {
                return new UserProvider(value);
            }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Value;
            }
        }
    }