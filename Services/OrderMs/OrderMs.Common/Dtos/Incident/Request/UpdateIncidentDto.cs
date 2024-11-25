using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Dtos.Request
{
    public record UpdateIncidentDto
    {
        public string? DestinyLocation { get; init; }
        public string? Description { get; init; }
        public DateTime? Date { get; init; }

    }
}