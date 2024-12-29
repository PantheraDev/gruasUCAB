using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderMs.Common.Exceptions
{
    public class NullAttributeException : Exception
    {
        public NullAttributeException() { }

        public NullAttributeException(string message)
            : base(message) { }

        public NullAttributeException(string message, Exception inner)
            : base(message, inner) { }
    }
}