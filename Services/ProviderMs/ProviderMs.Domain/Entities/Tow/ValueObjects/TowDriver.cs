using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public partial class TowDriver
    {
        private TowDriver(Guid value) => Value = value;
        public static TowDriver? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("TowDriver is required");

            return new TowDriver(value);
        }
        public Guid Value { get; init; }
    }
}