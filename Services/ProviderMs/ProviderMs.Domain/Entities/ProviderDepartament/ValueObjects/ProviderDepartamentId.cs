using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public class ProviderDepartmentId

    {
        private ProviderDepartmentId(Guid value) => Value = value;

        public static ProviderDepartmentId Create()
        {
            return new ProviderDepartmentId(Guid.NewGuid());
        }
        public static ProviderDepartmentId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("ProviderDepartment id is required");

            return new ProviderDepartmentId(value);
        }

        public Guid Value { get; init; }
    }
}