using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class FeeId

    {
        private FeeId(Guid value) => Value = value;

        public static FeeId Create()
        {
            return new FeeId(Guid.NewGuid());
        }
        public static FeeId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("Fee id is required");

            return new FeeId(value);
        }

        public Guid Value { get; init; }
    }
}