using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public partial class VehicleModel
    {
        private const string Pattern = @"^[a-zA-Z]+$";
        private VehicleModel(string value) => Value = value;

        public static VehicleModel Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Vehicle model is required");
                if (!ModelRegex().IsMatch(value)) throw new InvalidAttributeException("Vehicle model is invalid");

                return new VehicleModel(value);
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