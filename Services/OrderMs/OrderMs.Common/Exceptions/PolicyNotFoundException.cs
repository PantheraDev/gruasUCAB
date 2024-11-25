using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Exceptions
{
    public class PolicyNotFoundException : Exception
    {
        public PolicyNotFoundException() { }

        public PolicyNotFoundException(string message)
            : base(message) { }

        public PolicyNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}