using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public partial class InsuredVehicleModel
    {
        private const string Pattern = @"^[a-zA-Z]+$";
        private InsuredVehicleModel(string value) => Value = value;

        public static InsuredVehicleModel Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Vehicle model is required");
                if (!ModelRegex().IsMatch(value)) throw new InvalidAttributeException("Vehicle model is invalid");

                return new InsuredVehicleModel(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
        [GeneratedRegex(Pattern)]
        private static partial Regex ModelRegex();
    }
}