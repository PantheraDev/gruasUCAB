using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Dtos.Request
{
    public record UpdatePolicyDto
    {
        public decimal? Coverage { get; init; }
        public DateTime? ExpirationDate { get; init; }
        public DateTime? IssueDate { get; init; }
        public Guid? InsuredVehicleId { get; init; }
        public Guid? FeeId { get; init; }

    }
}