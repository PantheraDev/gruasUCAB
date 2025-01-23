using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Dtos.Request
{
    public record UpdateAdditionalCostDto
    {
        public decimal? Value { get; init; }
        public string? Description { get; init; }

    }
}