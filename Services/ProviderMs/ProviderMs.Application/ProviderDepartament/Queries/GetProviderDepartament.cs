using MediatR;
using ProviderMs.Common.dto.Response;

namespace ProviderMs.Application.Queries
{
    public class GetProviderDepartmentQuery : IRequest<GetProviderDepartment>
    {
        public Guid Id { get; set; }

        public GetProviderDepartmentQuery(Guid id)
        {
            Id = id;
        }
    }
}