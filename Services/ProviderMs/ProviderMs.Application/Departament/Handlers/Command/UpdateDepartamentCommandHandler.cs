using MediatR;
using ProviderMs.Application.Validators;
using ProviderMs.Common.Exceptions;
using ProviderMs.Common.Primitives;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Guid>
    {
        private readonly IDepartmentRepository _DepartmentRepository;
        public UpdateDepartmentCommandHandler(IDepartmentRepository DepartmentRepository)
        {
            _DepartmentRepository = DepartmentRepository ?? throw new ArgumentNullException(nameof(DepartmentRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldDepartment = await _DepartmentRepository.GetByIdAsync(DepartmentId.Create(request.Id)!);

                if (oldDepartment == null) throw new DepartmentNotFoundException("Department not found");


                if (request.Department.Name != null)
                {
                    oldDepartment = Department.Update(oldDepartment, DepartmentName.Create(request.Department.Name));
                }


                //TODO: Hay que hacer que se guarde el UpdatedBy

                await _DepartmentRepository.UpdateAsync(oldDepartment);

                return oldDepartment.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}