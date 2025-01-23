using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.Entities.ValueObjects
{
    public partial class FeeRadius
    {
        //private const string Pattern = @"^\d+\.\d{2}$";
        private FeeRadius(int value) => Value = value;

        public static FeeRadius Create(int value)
        {
            try
            {
                if (value == default) throw new NullAttributeException("Radius is required");
                //if (!RadiusRegex().IsMatch(value)) throw new InvalidAttributeException("Client ci is invalid");
                if (value < 0) throw new InvalidAttributeException("Radius is invalid");

                return new FeeRadius(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor

        // [GeneratedRegex(Pattern)]
        // private static partial Regex RadiusRegex();
    }
}