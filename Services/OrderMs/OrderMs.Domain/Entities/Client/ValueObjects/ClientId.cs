using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class ClientId

    {
        private ClientId(Guid value) => Value = value;

        public static ClientId Create()
        {
            return new ClientId(Guid.NewGuid());
        }
        public static ClientId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("Client id is required");

            return new ClientId(value);
        }

        public Guid Value { get; init; }
    }
}