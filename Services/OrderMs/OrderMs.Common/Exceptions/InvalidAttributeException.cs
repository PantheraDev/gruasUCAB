using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Exceptions
{
    public class InvalidAttributeException : Exception
    {
        public InvalidAttributeException() { }

        public InvalidAttributeException(string message)
            : base(message) { }

        public InvalidAttributeException(string message, Exception inner)
            : base(message, inner) { }
    }
}