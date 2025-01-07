using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Application.Validators;
using ProviderMs.Common.Primitives;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Guid>
    {
        private readonly IDepartmentRepository _DepartmentRepository;
        public DeleteDepartmentCommandHandler(IDepartmentRepository DepartmentRepository)
        {
            _DepartmentRepository = DepartmentRepository ?? throw new ArgumentNullException(nameof(DepartmentRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var departmentId = DepartmentId.Create(request.DepartmentId);
                await _DepartmentRepository.DeleteAsync(departmentId!);
                return departmentId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}