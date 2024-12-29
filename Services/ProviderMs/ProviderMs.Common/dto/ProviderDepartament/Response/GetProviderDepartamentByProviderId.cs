namespace ProviderMs.Common.dto.Response
{
    public class GetProviderDepartamentByProviderId
    {
        public Guid DepartamentId { get; set; }

        public GetProviderDepartamentByProviderId(Guid departamentId)
        {
            DepartamentId = departamentId;
        }
    }
}