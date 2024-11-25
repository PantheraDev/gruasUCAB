using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class InsuredVehicleId

    {
        private InsuredVehicleId(Guid value) => Value = value;

        public static InsuredVehicleId Create()
        {
            return new InsuredVehicleId(Guid.NewGuid());
        }
        public static InsuredVehicleId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("InsuredVehicle id is required");

            return new InsuredVehicleId(value);
        }

        public Guid Value { get; init; }
    }
}