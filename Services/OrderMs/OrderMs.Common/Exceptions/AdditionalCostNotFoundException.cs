using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Exceptions
{
    public class AdditionalCostNotFoundException : Exception
    {
        public AdditionalCostNotFoundException() { }

        public AdditionalCostNotFoundException(string message)
            : base(message) { }

        public AdditionalCostNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}