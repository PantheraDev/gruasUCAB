namespace ProviderMs.Common.dto.Request
{
    public record UpdateProviderDto
    {
        public string? Name { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Email { get; init; }
        public string? RIF { get; init; }
        public string? Address { get; init; }
    }
}