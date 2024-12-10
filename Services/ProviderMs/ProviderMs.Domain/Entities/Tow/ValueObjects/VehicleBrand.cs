using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public partial class VehicleBrand
    {
        private const string Pattern = @"^[a-zA-Z]+$";
        private VehicleBrand(string value) => Value = value;

        public static VehicleBrand Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Vehicle Brand is required");
                if (!BrandRegex().IsMatch(value)) throw new InvalidAttributeException("Vehicle Brand is invalid");

                return new VehicleBrand(value);
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