using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Exceptions
{
    public class OrderStateException : Exception
    {
        public OrderStateException() { }

        public OrderStateException(string message)
            : base(message) { }

        public OrderStateException(string message, Exception inner)
            : base(message, inner) { }
    }
}