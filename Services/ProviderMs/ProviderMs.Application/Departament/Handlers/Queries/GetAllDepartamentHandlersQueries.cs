using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.ApplicationQueries;
using ProviderMs.Common.dto.Request;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetAllDepartmentQueryHandler : IRequestHandler<GetAllDepartmentsQuery, List<GetDepartment>>
    {
        public IDepartmentRepository _departmentRepository;

        public GetAllDepartmentQueryHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<GetDepartment>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetAllAsync();

            if (department == null) throw new ProviderNotFoundException("department are empty");

            return department.Where(p => !p.IsDeleted).Select(p =>
                new GetDepartment(
                    p.Id.Value,
                    p.Name.Value,
                    p.CreatedBy
                )
            ).ToList();
        }
    }
}