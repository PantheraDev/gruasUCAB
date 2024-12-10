using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderMs.Common.Exceptions
{
    public class DepartamentNotFoundException : Exception
    {
        public DepartamentNotFoundException() { }

        public DepartamentNotFoundException(string message)
            : base(message) { }

        public DepartamentNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}