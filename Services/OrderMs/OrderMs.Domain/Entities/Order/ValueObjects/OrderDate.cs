using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.ValueObjects
{
    public class OrderDate
    {
        private OrderDate(DateTime value) => Value = value;

        public static OrderDate Create(DateTime value)
        {
            try
            {
                if (value == null) throw new NullAttributeException("Order Date is required");
                var today = DateTime.Today;

                if (value < today) throw new InvalidAttributeException("Order day is invalid");

                return new OrderDate(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DateTime Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor
    }
}