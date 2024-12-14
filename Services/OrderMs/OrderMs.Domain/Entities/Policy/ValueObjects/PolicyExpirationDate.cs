using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class PolicyExpirationDate
    {
        private PolicyExpirationDate(DateTime value) => Value = value;

        public static PolicyExpirationDate Create(DateTime value)
        {
            try
            {
                if (value == null) throw new NullAttributeException("Policy expiration date is required");
                var today = DateTime.Today;
                if (value.Year <= today.Year)
                    throw new InvalidAttributeException("policy expiration date is invalid");
                return new PolicyExpirationDate(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DateTime Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
    }
}