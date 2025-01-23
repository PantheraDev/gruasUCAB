using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Exceptions
{
    public class InsuredVehicleNotFoundException : Exception
    {
        public InsuredVehicleNotFoundException() { }

        public InsuredVehicleNotFoundException(string message)
            : base(message) { }

        public InsuredVehicleNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}