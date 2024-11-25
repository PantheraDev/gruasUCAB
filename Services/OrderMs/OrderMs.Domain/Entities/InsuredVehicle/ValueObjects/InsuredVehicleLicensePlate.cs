using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public partial class InsuredVehicleLicensePlate
    {
        private const string Pattern = @"^[a-zA-Z0-9]+$";
        private InsuredVehicleLicensePlate(string value) => Value = value;

        public static InsuredVehicleLicensePlate Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Vehicle license plate is required");
                if (!LicensePlateRegex().IsMatch(value)) throw new InvalidAttributeException("Vehicle license plate is invalid");

                return new InsuredVehicleLicensePlate(value);
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