using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class ClientBirthDate
    {
        private ClientBirthDate(DateTime value) => Value = value;

        public static ClientBirthDate Create(DateTime value)
        {
            try
            {
                if (value == null) throw new NullAttributeException("Client Birth Date is required");
                var today = DateTime.Today;
                var age = today.Year - value.Year;

                if (value.Date > today.AddYears(-age))
                    age--;

                if (age < 18) throw new InvalidAttributeException("Client birth day is invalid");

                return new ClientBirthDate(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DateTime Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
    }
}