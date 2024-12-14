using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public partial class ClientCi
    {
        private const string Pattern = @"^\d{7,8}$";
        private ClientCi(string value) => Value = value;

        public static ClientCi Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Client ci is required");
                if (!CiRegex().IsMatch(value)) throw new InvalidAttributeException("Client ci is invalid");

                return new ClientCi(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor

        [GeneratedRegex(Pattern)]
        private static partial Regex CiRegex();
    }
}