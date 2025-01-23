namespace ProviderMs.Common.dto.Request
{
    public record CreateProviderDepartmentdto
    {
        public Guid ProviderId { get; init; }
        public Guid DepartmentId { get; init; }
    }
}