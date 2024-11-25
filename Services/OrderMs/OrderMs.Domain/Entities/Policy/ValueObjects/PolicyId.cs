using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class PolicyId

    {
        private PolicyId(Guid value) => Value = value;

        public static PolicyId Create()
        {
            return new PolicyId(Guid.NewGuid());
        }
        public static PolicyId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("Policy id is required");

            return new PolicyId(value);
        }

        public Guid Value { get; init; }
    }
}