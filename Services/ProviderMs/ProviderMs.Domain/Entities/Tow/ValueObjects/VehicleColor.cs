using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public partial class VehicleColor
    {
        private const string Pattern = @"^[a-zA-Z]+$";
        private VehicleColor(string value) => Value = value;

        public static VehicleColor Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Vehicle color is required");
                if (!ColorRegex().IsMatch(value)) throw new InvalidAttributeException("Vehicle color is invalid");

                return new VehicleColor(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
        [GeneratedRegex(Pattern)]
        private static partial Regex ColorRegex();
    }
}