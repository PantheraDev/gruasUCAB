using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public partial class ProviderRIF
    {
        private const string Pattern = @"^[VEPGJ]\d{6,9}-?\d?$";

        private ProviderRIF(string value) => Value = value;

        public static ProviderRIF Create(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Provider RIF is required");
                if (!RIFRegex().IsMatch(value)) throw new InvalidAttributeException("Provider RIF is invalid");

                return new ProviderRIF(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor

        [GeneratedRegex(Pattern)]
        private static partial Regex RIFRegex();
    }
}