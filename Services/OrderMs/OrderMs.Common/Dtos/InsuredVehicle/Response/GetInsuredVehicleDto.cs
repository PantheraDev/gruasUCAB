
namespace OrderMs.Common.Dtos.Response
{
    public class GetInsuredVehicleDto
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public decimal Weight { get; set; }
        public string Year { get; set; }
        //TODO: Puede retornar el dto de client?
        public Guid ClientId { get; init; }
        public string? CreatedBy { get; set; }

        public GetInsuredVehicleDto(Guid id, string? createdBy, string licensePlate, string brand, string model, string color, decimal weight, string year, Guid clientId)
        {
            Id = id;
            CreatedBy = createdBy;
            LicensePlate = licensePlate;
            Brand = brand;
            Model = model;
            Color = color;
            Weight = weight;
            Year = year;
            ClientId = clientId;
        }

    }
}