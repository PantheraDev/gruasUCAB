

namespace OrderMs.Common.Dto.Response
{
    public class GetTow
    {

        public Guid id { get; set; }
        public string color { get; set; }
        public string year { get; set; }
        public string model { get; set; }
        public string brand { get; set; }
        public string licensePlate { get; set; }
        public string towLocation { get; set; }
        public int towAvailability { get; set; }
        public int towType { get; set; }
        public string providerId { get; set; }
        public string? towDriver { get; set; }
        public string? createdBy { get; set; }

        public GetTow() { }
        public GetTow(Guid id, string color, string year, string model, string brand, string licensePlate, string towLocation, int towAvailability, int towType, string providerId, string? towDriver, string? createdBy)
        {
            this.id = id;
            this.color = color;
            this.year = year;
            this.model = model;
            this.brand = brand;
            this.licensePlate = licensePlate;
            this.towLocation = towLocation;
            this.towAvailability = towAvailability;
            this.towType = towType;
            this.providerId = providerId;
            this.towDriver = towDriver;
            this.createdBy = createdBy;
        }
    }
}
