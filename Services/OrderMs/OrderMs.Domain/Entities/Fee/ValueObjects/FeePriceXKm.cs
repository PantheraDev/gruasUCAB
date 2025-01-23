using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.Entities.ValueObjects
{
    public partial class FeePriceXKm
    {
        private const string Pattern = @"^\d+\.\d{2}$";
        private FeePriceXKm(decimal value) => Value = value;

        public static FeePriceXKm Create(decimal value)
        {
            try
            {
                if (value == default) throw new NullAttributeException("Base price is required");
                //if (!PriceXKmRegex().IsMatch(value)) throw new InvalidAttributeException("Client ci is invalid");
                if (value < 0) throw new InvalidAttributeException("Base price is invalid");

                return new FeePriceXKm(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public decimal Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor

        [GeneratedRegex(Pattern)]
        private static partial Regex PriceXKmRegex();
    }
}