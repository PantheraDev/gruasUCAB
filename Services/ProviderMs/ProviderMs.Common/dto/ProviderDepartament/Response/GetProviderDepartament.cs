namespace ProviderMs.Common.dto.Response
{
    public class GetProviderDepartment
    {
        public Guid Id { get; set; }
        public Guid ProviderId { get; set; }
        public Guid DepartmentId { get; set; }

        public GetProviderDepartment(Guid id, Guid providerId, Guid departmentId)
        {
            Id = id;
            ProviderId = providerId;
            DepartmentId = departmentId;
        }
    }
}