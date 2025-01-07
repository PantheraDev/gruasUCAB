using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MediatR;
using Microsoft.VisualBasic;
using ProviderMs.Application.Command;
using ProviderMs.Application.Validators;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Guid>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateDepartmentValidator();
                await validator.ValidateRequest(request.Department);
                var departmentId = DepartmentId.Create(Guid.NewGuid());
                var departmentName = DepartmentName.Create(request.Department.Name);
                var department = new Department(departmentId, departmentName);
                await _departmentRepository.AddAsync(department);
                return department.Id.Value;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}