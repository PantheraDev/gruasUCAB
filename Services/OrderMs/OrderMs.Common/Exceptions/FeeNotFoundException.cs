using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Exceptions
{
    public class FeeNotFoundException : Exception
    {
        public FeeNotFoundException() { }

        public FeeNotFoundException(string message)
            : base(message) { }

        public FeeNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}