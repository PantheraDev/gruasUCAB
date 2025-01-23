using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public partial class ClientPhone
    {
        //private const int DefaultLenght = 11;
        private const string Pattern = @"^\d{11}$";

        private ClientPhone(string value) => Value = value;

        public static ClientPhone Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Client phone is required");
                if (!PhoneNumberRegex().IsMatch(value)) throw new InvalidAttributeException("Client phone is invalid");

                return new ClientPhone(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor

        [GeneratedRegex(Pattern)]
        private static partial Regex PhoneNumberRegex();


    }
}