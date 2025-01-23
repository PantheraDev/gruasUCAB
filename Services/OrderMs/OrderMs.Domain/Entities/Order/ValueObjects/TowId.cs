using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class TowId

    {
        private TowId(Guid value) => Value = value;

        public static TowId Create()
        {
            return new TowId(Guid.NewGuid());
        }
        public static TowId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("Tow id is required");

            return new TowId(value);
        }

        public Guid Value { get; init; }
    }
}