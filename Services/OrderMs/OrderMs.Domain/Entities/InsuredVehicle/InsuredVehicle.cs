using OrderMs.Common.Primitives;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Domain.Entities
{
    //* Porque es sellada? Porque no necesito decir que esta clase tenga una modificacion externa y que todo su comportamiento este dentro de ella

    public sealed class InsuredVehicle : AggregateRoot
    {
        public InsuredVehicleId Id { get; private set; }
        public InsuredVehicleWeight Weight { get; private set; }
        public InsuredVehicleLicensePlate LicensePlate { get; private set; }
        public InsuredVehicleBrand Brand { get; private set; }
        public InsuredVehicleModel Model { get; private set; }
        public InsuredVehicleYear Year { get; private set; }
        public InsuredVehicleColor Color { get; private set; }
        public ClientId ClientId { get; private set; }
        //TODO: Ver si hace falta lo comentado
        public Client? Client { get; private set; }

        public InsuredVehicle(InsuredVehicleId id, InsuredVehicleWeight weight, InsuredVehicleLicensePlate licensePlate, InsuredVehicleBrand brand, InsuredVehicleModel model, InsuredVehicleYear year, InsuredVehicleColor color, ClientId clientId, Client? client = null)
        {
            Id = id;
            Weight = weight;
            LicensePlate = licensePlate;
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            ClientId = clientId;
            Client = client;
        }

        public InsuredVehicle() { }

        public static InsuredVehicle Update(InsuredVehicle InsuredVehicle, InsuredVehicleWeight? weight, InsuredVehicleLicensePlate? licensePlate, InsuredVehicleBrand? brand, InsuredVehicleModel? model, InsuredVehicleYear? year, InsuredVehicleColor? color, ClientId? client)
        {
            // TODO: Esto podria solucionarse haciendo un DTO
            var updates = new List<Action>{
                    () => { if (weight != null) InsuredVehicle.Weight = weight; },
                    () => { if (licensePlate != null) InsuredVehicle.LicensePlate = licensePlate; },
                    () => { if (brand != null) InsuredVehicle.Brand = brand; },
                    () => { if (model != null) InsuredVehicle.Model = model; },
                    () => { if (year != null) InsuredVehicle.Year = year; },
                    () => { if (color != null) InsuredVehicle.Color = color; },
                    () => { if (client != null) InsuredVehicle.ClientId = client; }
                };

            updates.ForEach(update => update());
            return InsuredVehicle;
        }
    }
}