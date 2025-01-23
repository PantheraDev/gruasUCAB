using MediatR;
using ProviderMs.Common.dto.Response;

namespace ProviderMs.ApplicationQueries
{
    public class GetAllProviderDepartmentsQuery : IRequest<List<GetProviderDepartment>>
    {
        public GetAllProviderDepartmentsQuery()
        {
        }
    }
}