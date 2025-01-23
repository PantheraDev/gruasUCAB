    namespace UserMs.Domain.Entities
    {
        public class UserDepartament : ValueObject
        {
            public Guid Value { get; }

            private UserDepartament(Guid value)
            {
                Value = value;
            }

            public static UserDepartament Create(Guid value)
            {
                return new UserDepartament(value);
            }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Value;
            }
        }
    }