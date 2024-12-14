using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Dtos.Request
{
    public record UpdateInsuredVehicleDto
    {
        public string? LicensePlate { get; init; }
        public string? Brand { get; init; }
        public string? Model { get; init; }
        public string? Color { get; init; }
        public decimal? Weight { get; init; }
        public string? Year { get; init; }
        public Guid? ClientId { get; init; }
    }
}