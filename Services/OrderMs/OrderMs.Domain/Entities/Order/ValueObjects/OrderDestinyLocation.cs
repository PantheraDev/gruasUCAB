using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public partial class OrderDestinyLocation
    {
        //private const string Pattern = @"^[a-zA-Z]+$";
        private OrderDestinyLocation(string value) => Value = value;

        public static OrderDestinyLocation Create(string value)
        {
            try
            {
                //TODO Aqui se deberia saber si la direccion es valida o no????
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Client Name is required");
                //if (!NameRegex().IsMatch(value)) throw new InvalidAttributeException("Client Name is invalid");

                return new OrderDestinyLocation(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
        // [GeneratedRegex(Pattern)]
        // private static partial Regex NameRegex();
    }
}