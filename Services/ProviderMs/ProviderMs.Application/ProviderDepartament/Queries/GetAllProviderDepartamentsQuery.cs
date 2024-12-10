using MediatR;
using ProviderMs.Common.dto.Response;

namespace ProviderMs.ApplicationQueries
{
    public class GetAllProviderDepartamentsQuery : IRequest<List<GetProviderDepartament>>
    {
        public GetAllProviderDepartamentsQuery() { }
    }
}