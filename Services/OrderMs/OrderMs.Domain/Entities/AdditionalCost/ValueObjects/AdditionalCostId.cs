using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class AdditionalCostId

    {
        private AdditionalCostId(Guid value) => Value = value;

        public static AdditionalCostId Create()
        {
            return new AdditionalCostId(Guid.NewGuid());
        }
        public static AdditionalCostId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("Additional Cost id is required");

            return new AdditionalCostId(value);
        }

        public Guid Value { get; init; }
    }
}