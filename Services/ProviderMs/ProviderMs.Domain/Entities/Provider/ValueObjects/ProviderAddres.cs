using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public class ProviderAddress
    {
        private ProviderAddress(string value) => Value = value;

        public static ProviderAddress Create(string value)
        {

            if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Provider address is required");

            return new ProviderAddress(value);
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
    }
}