using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public partial class ProviderPhone
    {
        //private const int DefaultLenght = 11;
        private const string Pattern = @"^\d{11}$";

        private ProviderPhone(string value) => Value = value;

        public static ProviderPhone Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Provider phone is required");
                if (!PhoneNumberRegex().IsMatch(value)) throw new InvalidAttributeException("Provider phone is invalid");

                return new ProviderPhone(value);
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