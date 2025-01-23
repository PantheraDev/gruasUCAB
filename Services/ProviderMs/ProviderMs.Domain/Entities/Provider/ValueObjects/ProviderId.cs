using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public class ProviderId

    {
        private ProviderId(Guid value) => Value = value;

        public static ProviderId Create()
        {
            return new ProviderId(Guid.NewGuid());
        }
        public static ProviderId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("Provider id is required");

            return new ProviderId(value);
        }

        public static ProviderId Create(object value)
        {
            throw new NotImplementedException();
        }

        public Guid Value { get; init; }
    }
}