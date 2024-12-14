using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class IncidentDate
    {
        private IncidentDate(DateTime value) => Value = value;

        public static IncidentDate Create(DateTime value)
        {
            try
            {
                if (value == null) throw new NullAttributeException("Incident Date is required");
                var today = DateTime.Today;

                if (value < today) throw new InvalidAttributeException("Incident day is invalid");

                return new IncidentDate(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DateTime Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
    }
}