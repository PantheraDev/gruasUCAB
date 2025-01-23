using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.Entities.ValueObjects
{
    public partial class FeeBasePrice
    {
        private const string Pattern = @"^\d+\.\d{2}$";
        private FeeBasePrice(decimal value) => Value = value;

        public static FeeBasePrice Create(decimal value)
        {
            try
            {
                if (value == default) throw new NullAttributeException("Base price is required");
                //if (!BasePriceRegex().IsMatch(value)) throw new InvalidAttributeException("Client ci is invalid");
                if (value < 0) throw new InvalidAttributeException("Base price is invalid");

                return new FeeBasePrice(value);
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