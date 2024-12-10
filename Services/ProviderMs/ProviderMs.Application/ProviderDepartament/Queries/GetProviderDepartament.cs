using MediatR;
using ProviderMs.Common.dto.Response;

namespace ProviderMs.Application.Queries
{
    public class GetProviderDepartamentQuery : IRequest<List<GetProviderDepartamentByProviderId>>
    {
        public Guid Id { get; set; }

        public GetProviderDepartamentQuery(Guid id)
        {
            Id = id;
        }
    }
}