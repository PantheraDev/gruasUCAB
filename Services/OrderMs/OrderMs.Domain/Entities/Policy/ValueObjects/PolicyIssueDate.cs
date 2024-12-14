using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class PolicyIssueDate
    {
        private PolicyIssueDate(DateTime value) => Value = value;

        public static PolicyIssueDate Create(DateTime value)
        {
            try
            {
                if (value == null) throw new NullAttributeException("Policy issue date is required");
                var today = DateTime.Today;
                if (value.Year > today.Year || value.Month > today.Month || value.Day > today.Day)
                    throw new InvalidAttributeException("Policy issue day is invalid");

                return new PolicyIssueDate(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DateTime Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
    }
}