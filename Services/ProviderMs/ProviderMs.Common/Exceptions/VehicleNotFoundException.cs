using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderMs.Common.Exceptions
{
    public class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException() { }

        public VehicleNotFoundException(string message)
            : base(message) { }

        public  VehicleNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}