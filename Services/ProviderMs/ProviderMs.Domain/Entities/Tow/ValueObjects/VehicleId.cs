using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public class VehicleId

    {
        private VehicleId(Guid value) => Value = value;

        public static VehicleId Create()
        {
            return new VehicleId(Guid.NewGuid());
        }
        public static VehicleId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("Vehicle id is required");

            return new VehicleId(value);
        }

        public Guid Value { get; init; }
    }
}