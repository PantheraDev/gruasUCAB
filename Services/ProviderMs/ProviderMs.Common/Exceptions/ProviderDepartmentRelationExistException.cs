using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderMs.Common.Exceptions
{
    public class ProviderDepartmentRelationExistException : Exception
    {
        public ProviderDepartmentRelationExistException() { }

        public ProviderDepartmentRelationExistException(string message)
            : base(message) { }

        public ProviderDepartmentRelationExistException(string message, Exception inner)
            : base(message, inner) { }
    }
}