using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public partial class ProviderName
    {
        private const string Pattern = @"^[a-zA-Z]+$";
        private ProviderName(string value) => Value = value;

        public static ProviderName Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Provider Name is required");
                if (!NameRegex().IsMatch(value)) throw new InvalidAttributeException("Provider Name is invalid");

                return new ProviderName(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
        [GeneratedRegex(Pattern)]
        private static partial Regex NameRegex();
    }
}