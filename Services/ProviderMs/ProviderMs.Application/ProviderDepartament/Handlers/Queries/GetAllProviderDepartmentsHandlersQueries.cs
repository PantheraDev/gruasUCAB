using MediatR;
using ProviderMs.ApplicationQueries;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetAllProviderDepartmentsQueryHandler : IRequestHandler<GetAllProviderDepartmentsQuery, List<GetProviderDepartment>>
    {
        public IProviderDepartmentRepository _providerDepartmentRepository;

        public GetAllProviderDepartmentsQueryHandler(IProviderDepartmentRepository providerDepartmentRepository)
        {
            _providerDepartmentRepository = providerDepartmentRepository;
        }

        public async Task<List<GetProviderDepartment>> Handle(GetAllProviderDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var providerDepartment = await _providerDepartmentRepository.GetAllAsync();

            if (providerDepartment == null) throw new ProviderNotFoundException("ProviderDepartments are empty");

            return providerDepartment.Where(p => !p.IsDeleted).Select(p =>
                new GetProviderDepartment(
                    p.Id.Value,
                    p.ProviderId.Value,
                    p.DepartmentId.Value
                )
            ).ToList();
        }
    }
}