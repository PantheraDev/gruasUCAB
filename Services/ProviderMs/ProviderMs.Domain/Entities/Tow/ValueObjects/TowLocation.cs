using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public partial class TowLocation
    {
        //private const string Pattern = @"^[a-zA-Z]+$";
        private TowLocation(string value) => Value = value;

        public static TowLocation Create(string value)
        {
            try
            {
                //TODO Aqui se deberia saber si la direccion es valida o no????
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Tow location is required");
                //if (!NameRegex().IsMatch(value)) throw new InvalidAttributeException("Client Name is invalid");

                return new TowLocation(value);
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