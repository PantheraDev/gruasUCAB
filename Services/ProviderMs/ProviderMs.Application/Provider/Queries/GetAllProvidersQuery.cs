using MediatR;
using ProviderMs.Common.dto.Response;

namespace ProviderMs.ApplicationQueries
{
    public class GetAllProvidersQuery : IRequest<List<GetProvider>>
    {
        public GetAllProvidersQuery() { }
    }
}