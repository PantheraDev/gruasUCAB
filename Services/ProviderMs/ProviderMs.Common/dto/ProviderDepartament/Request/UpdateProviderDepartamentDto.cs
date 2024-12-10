namespace ProviderMs.Common.dto.Request
{
    public record UpdateProviderDepartamentDto
    {
        public Guid? ProviderId { get; init; }
        public Guid DepartamentId { get; init; }
    }
}