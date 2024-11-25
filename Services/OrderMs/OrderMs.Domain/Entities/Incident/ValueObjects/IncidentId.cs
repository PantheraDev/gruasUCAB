using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class IncidentId

    {
        private IncidentId(Guid value) => Value = value;

        public static IncidentId Create()
        {
            return new IncidentId(Guid.NewGuid());
        }
        public static IncidentId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("Incident id is required");

            return new IncidentId(value);
        }

        public Guid Value { get; init; }
    }
}