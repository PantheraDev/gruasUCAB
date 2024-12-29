namespace ProviderMs.Common.dto.Response
{
    public class GetProviderDepartament
    {
        public Guid ProviderId { get; set; }
        public Guid DepartamentId { get; set; }

        public GetProviderDepartament(Guid providerId, Guid departamentId)
        {
            ProviderId = providerId;
            DepartamentId = departamentId;
        }
    }
}