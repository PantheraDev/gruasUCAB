using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Dtos.Request
{
    public record UpdateFeeDto
    {
        public decimal? BasePrice { get; init; }
        public int? Radius { get; init; }
        public decimal? PriceXKm { get; init; }

    }
}