using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public partial class InsuredVehicleBrand
    {
        private const string Pattern = @"^[a-zA-Z]+$";
        private InsuredVehicleBrand(string value) => Value = value;

        public static InsuredVehicleBrand Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Vehicle Brand is required");
                if (!BrandRegex().IsMatch(value)) throw new InvalidAttributeException("Vehicle Brand is invalid");

                return new InsuredVehicleBrand(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
        [GeneratedRegex(Pattern)]
        private static partial Regex BrandRegex();
    }
}