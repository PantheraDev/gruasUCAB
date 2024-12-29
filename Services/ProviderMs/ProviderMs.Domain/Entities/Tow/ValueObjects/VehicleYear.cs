using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public partial class VehicleYear
    {
        private const string Pattern = @"^\d{4}$";
        private VehicleYear(string value) => Value = value;

        public static VehicleYear Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Vehicle year is required");
                if (!YearRegex().IsMatch(value)) throw new InvalidAttributeException("Vehicle year is invalid");
                if (int.Parse(value) < 1950 || int.Parse(value) > DateTime.Now.Year) throw new InvalidAttributeException("Vehicle year is invalid");

                return new VehicleYear(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
        [GeneratedRegex(Pattern)]
        private static partial Regex YearRegex();
    }
}