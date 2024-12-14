using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public partial class ClientLastName
    {
        private const string Pattern = @"^[a-zA-Z]+$";
        private ClientLastName(string value) => Value = value;

        public static ClientLastName Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Client last name is required");
                if (!LastNameRegex().IsMatch(value)) throw new InvalidAttributeException("Client last name is invalid");

                return new ClientLastName(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor

        [GeneratedRegex(Pattern)]
        private static partial Regex LastNameRegex();
    }
}