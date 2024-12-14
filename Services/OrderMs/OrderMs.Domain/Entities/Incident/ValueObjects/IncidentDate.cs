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
                if (value.Year > today.Year || value.Month > today.Month || value.Day > today.Day) throw new InvalidAttributeException("Incident date is invalid");

                return new IncidentDate(value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DateTime Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
    }
}