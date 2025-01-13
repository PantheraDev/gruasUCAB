namespace ProviderMs.Common.dto.Request
{
    public record CreateTowdto
    {   
        public string Color { get; init; } = String.Empty;
        public string Year { get; init; } = String.Empty;
        public string Model { get; init; } = String.Empty;
        public string Brand { get; init; } = String.Empty;
        public string LicensePlate { get; init; } = String.Empty;
        public string TowLocation {get; init;} = String.Empty;
        public string TowAvailability {get; init;} = String.Empty;
        public string TowType {get; init;} = String.Empty;
        public Guid ProviderId {get; init;}
        //public Guid? TowDriver {get; init;}
    } 
}