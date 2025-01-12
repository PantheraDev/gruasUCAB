using MediatR;
using ProviderMs.Common.dto.Response;

namespace ProviderMs.ApplicationQueries
{
    public class GetDepartmentsByProviderQuery : IRequest<List<GetProviderDepartment>>
    {
        public Guid ProviderId { get; set; }
        public GetDepartmentsByProviderQuery(Guid providerId)
        {
            ProviderId = providerId;
        }
    }
}