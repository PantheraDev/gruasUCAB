using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public partial class VehicleLicensePlate
    {
        private const string Pattern = @"^[a-zA-Z0-9]+$";
        private VehicleLicensePlate(string value) => Value = value;

        public static VehicleLicensePlate Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Vehicle license plate is required");
                if (!LicensePlateRegex().IsMatch(value)) throw new InvalidAttributeException("Vehicle license plate is invalid");

                return new VehicleLicensePlate(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
        [GeneratedRegex(Pattern)]
        private static partial Regex LicensePlateRegex();
    }
}