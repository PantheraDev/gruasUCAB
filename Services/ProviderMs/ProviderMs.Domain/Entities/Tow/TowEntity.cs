using ProviderMs.Domain.ValueObjects;
using ProviderMs.Common.Primitives;
using ProviderMs.Domain;
using ProviderMs.Common.Enums;
using ProviderMs.Domain.ValueObjects;
using ProviderMs.Domain.Interfaces;



namespace ProviderMs.Domain.Entities;

public  sealed class Tow : AggregateRoot, IVehicle
{
    public VehicleId Id {get; private set;}
    public VehicleColor Color {get; private set;}
    public VehicleYear Year {get; private set;}
    public VehicleModel Model {get; private set;}
    public VehicleBrand Brand {get; private set;}
    public VehicleLicensePlate LicensePlate {get; private set;}
    public TowLocation TowLocation {get; private set;}
    public TowAvailibility TowAvailability {get; private set;}
    public TowType TowType {get; private set;}

    public ProviderId ProviderId {get; private set;}
    public Provider provider {get; set;} 



    public Tow (VehicleId id, VehicleColor color, VehicleYear year, VehicleModel model, VehicleBrand brand, VehicleLicensePlate licensePlate, TowLocation towLocation, TowAvailibility towAvailibility, TowType towType, ProviderId providerId)
    {
        Id = id;
        Color = color;
        Year = year;
        Model = model;
        Brand = brand;
        LicensePlate = licensePlate;
        TowLocation = towLocation;
        TowAvailability = towAvailibility;
        TowType = towType;
        ProviderId = providerId;
    }

    public Tow() { }

    public static Tow Update(Tow tow, VehicleColor? color, VehicleYear? year, VehicleModel? model, VehicleBrand? brand, VehicleLicensePlate? licensePlate, TowLocation? towLocation, TowAvailibility? towAvailibility, TowType? towType){

            var updates = new List<Action>{
                () => {if(color !=null) tow.Color = color;},
                () => {if(year !=null)tow.Year = year;},
                () => {if(model !=null)tow.Model = model;},
                () => {if(brand !=null)tow.Brand = brand;},
                () => {if(licensePlate !=null)tow.LicensePlate = licensePlate;},
                () => {if(towLocation !=null)tow.TowLocation = towLocation;},
                () => {if(towAvailibility !=null)tow.TowAvailability = towAvailibility;},
                () => {if(towType !=null)tow.TowType = towType.Value;},
            };
            updates.ForEach(update => update());
            return tow;
    }
}