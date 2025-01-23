using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.Entities.ValueObjects
{
    public partial class AdditionalCostValue
    {
        private const string Pattern = @"^\d+\.\d{2}$";
        private AdditionalCostValue(decimal value) => Value = value;
        //TODO REvisar si necesita el regex
        public static AdditionalCostValue Create(decimal value)
        {
            try
            {
                if (value == default) throw new NullAttributeException("Value is required");
                //if (!BasePriceRegex().IsMatch(value)) throw new InvalidAttributeException("Client ci is invalid");
                if (value < 0) throw new InvalidAttributeException("Value is invalid");

                return new AdditionalCostValue(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public decimal Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor

        [GeneratedRegex(Pattern)]
        private static partial Regex BasePriceRegex();
    }
}