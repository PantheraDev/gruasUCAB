using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderMs.Common.dto.Request
{
    public record UpdateTowDto
    {
        public string? Color { get; init; }
        public string? Year { get; init; }
        public string? Model { get; init; }
        public string? Brand { get; init; }
        public string? LicensePlate { get; init; }
        public string? TowLocation {get; init;}
        public bool TowAvailability {get; init;}
        public string? TowType {get; init;}
    }
}