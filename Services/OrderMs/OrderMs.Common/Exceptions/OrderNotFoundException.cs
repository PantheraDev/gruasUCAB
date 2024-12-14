using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() { }

        public OrderNotFoundException(string message)
            : base(message) { }

        public OrderNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}