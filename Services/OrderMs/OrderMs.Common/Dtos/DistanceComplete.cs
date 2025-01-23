using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Dtos
{
    public class DistanceComplete
    {
        public string Distance { get; set; } = String.Empty;
        public int DistanceValue { get; set; }
        public string Eta { get; set; } = String.Empty;
    }
}