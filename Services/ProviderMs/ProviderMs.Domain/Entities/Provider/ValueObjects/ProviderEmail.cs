using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public partial class ProviderEmail
    {
        //private const int DefaultLenght = 11;
        private const string Pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";

        private ProviderEmail(string value) => Value = value;

        public static ProviderEmail Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Provider Email is required");
                if (!PhoneNumberRegex().IsMatch(value)) throw new InvalidAttributeException("Provider Email is invalid");

                return new ProviderEmail(value);
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