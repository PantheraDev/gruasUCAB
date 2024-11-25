using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class ClientAddress
    {
        private ClientAddress(string value) => Value = value;

        public static ClientAddress Create(string value)
        {

            if (string.IsNullOrEmpty(value)) throw new NullAttributeException("Client address is required");

            return new ClientAddress(value);
        }

        public string Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
    }
}