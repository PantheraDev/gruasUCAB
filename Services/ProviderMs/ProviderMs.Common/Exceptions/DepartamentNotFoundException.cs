using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderMs.Common.Exceptions
{
    public class DepartmentNotFoundException : Exception
    {
        public DepartmentNotFoundException() { }

        public DepartmentNotFoundException(string message)
            : base(message) { }

        public DepartmentNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}