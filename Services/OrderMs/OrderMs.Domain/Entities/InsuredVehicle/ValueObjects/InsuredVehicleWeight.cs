using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.Entities.ValueObjects
{
    public partial class InsuredVehicleWeight
    {
        private InsuredVehicleWeight(decimal value) => Value = value;

        public static InsuredVehicleWeight Create(decimal value)
        {
            try
            {
                if (value == default) throw new NullAttributeException("Weight is required");
                //if (!BasePriceRegex().IsMatch(value)) throw new InvalidAttributeException("Client ci is invalid");
                if (value < 0) throw new InvalidAttributeException("Weight is invalid");

                return new InsuredVehicleWeight(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public decimal Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor

    }
}