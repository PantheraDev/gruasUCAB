namespace ProviderMs.Common.dto.Request
{
    public record UpdateProviderDepartmentDto
    {
        public Guid? ProviderId { get; init; }
        public Guid? DepartmentId { get; init; }
    }
}