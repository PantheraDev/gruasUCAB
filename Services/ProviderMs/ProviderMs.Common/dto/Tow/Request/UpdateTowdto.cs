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
        public string TowAvailability {get; init;}
        public string? TowType {get; init;}
        public Guid ProviderId {get; init;}
        //public Guid TowDriver {get; init;}
    }
}