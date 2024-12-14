using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.Entities.ValueObjects
{
    public partial class PolicyCoverage
    {
        private const string Pattern = @"^\d+\.\d{2}$";
        private PolicyCoverage(decimal value) => Value = value;

        public static PolicyCoverage Create(decimal value)
        {
            try
            {
                if (value == default) throw new NullAttributeException("Coverage is required");
                //if (!BasePriceRegex().IsMatch(value)) throw new InvalidAttributeException("Client ci is invalid");
                if (value < 0) throw new InvalidAttributeException("Coverage is invalid");

                return new PolicyCoverage(value);
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