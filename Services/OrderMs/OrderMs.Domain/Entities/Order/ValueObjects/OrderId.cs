using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class OrderId

    {
        private OrderId(Guid value) => Value = value;

        public static OrderId Create()
        {
            return new OrderId(Guid.NewGuid());
        }
        public static OrderId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("Order id is required");

            return new OrderId(value);
        }

        public Guid Value { get; init; }
    }
}