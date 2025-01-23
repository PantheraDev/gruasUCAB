using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Application.Queries;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, GetDepartment>
    {
        public IDepartmentRepository _departmentRepository;

        public GetDepartmentQueryHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<GetDepartment> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("department id is required");
            var departmentId = DepartmentId.Create(request.Id);
            var department = await _departmentRepository.GetByIdAsync(departmentId!);

            if (department == null || department.IsDeleted) throw new ProviderNotFoundException("department not found");

            return new GetDepartment(
                department.Id.Value,
                department.Name.Value,
                department.CreatedBy
            );
        }
    }
}