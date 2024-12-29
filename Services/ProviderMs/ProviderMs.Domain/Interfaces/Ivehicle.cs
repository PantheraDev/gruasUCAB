using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Domain.Interfaces
{
    public interface IVehicle
    {
        VehicleId Id { get; }
        VehicleColor Color { get; }
        VehicleYear Year { get; }
        VehicleModel Model { get; }
        VehicleBrand Brand { get; }
        VehicleLicensePlate LicensePlate { get; }
    }
}