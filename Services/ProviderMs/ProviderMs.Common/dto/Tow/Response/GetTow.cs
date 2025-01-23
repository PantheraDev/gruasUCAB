using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Common.Enums;

namespace ProviderMs.Common.dto.Response
{
    public class GetTow
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
        public string Year { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string LicensePlate { get; set; }
        public string TowLocation { get; set; }
        public TowAvailability TowAvailability { get; init; }
        public TowType TowType { get; set; }
        public Guid ProviderId { get; init; }
        public Guid? TowDriver { get; set; }
        public string? CreatedBy { get; set; }

        public GetTow(Guid id, string color, string year, string model, string brand, string licensePlate, string towLocation, TowAvailability towAvailability, TowType towType, Guid providerId, Guid? towDriver, string? createdBy)
        {
            Id = id;
            Color = color;
            Year = year;
            Model = model;
            Brand = brand;
            LicensePlate = licensePlate;
            TowLocation = towLocation;
            TowAvailability = towAvailability;
            TowType = towType;
            ProviderId = providerId;
            TowDriver = towDriver;
            CreatedBy = createdBy;
        }
    }
}
