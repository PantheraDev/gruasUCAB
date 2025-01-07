using MediatR;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    public class UpdateProviderDepartmentCommandHandler : IRequestHandler<UpdateProviderDepartmentCommand, Guid>
    {
        private readonly IProviderDepartmentRepository _providerDepartmentRepository;
        public UpdateProviderDepartmentCommandHandler(IProviderDepartmentRepository providerDepartmentRepository)
        {
            _providerDepartmentRepository = providerDepartmentRepository ?? throw new ArgumentNullException(nameof(providerDepartmentRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(UpdateProviderDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldProviderDepartment = await _providerDepartmentRepository.GetByIdAsync(ProviderDepartmentId.Create(request.Id)!);

                if (oldProviderDepartment == null) throw new ProviderNotFoundException("providerDepartment not found");

                ProviderId provider = oldProviderDepartment.ProviderId;
                DepartmentId department = oldProviderDepartment.DepartmentId;

                if (request.ProviderDepartment.ProviderId != null)
                {
                    provider = ProviderId.Create(request.ProviderDepartment.ProviderId.Value)!;
                }
                if (request.ProviderDepartment.DepartmentId != null)
                {
                    department = DepartmentId.Create(request.ProviderDepartment.DepartmentId.Value)!;
                }

                var newProviderDepartment = new ProviderDepartment(
                    oldProviderDepartment.Id,
                    provider,
                    department
                );

                if (await _providerDepartmentRepository.RelationExistAsync(
                    newProviderDepartment.ProviderId,
                    newProviderDepartment.DepartmentId)
                    ) throw new ProviderDepartmentRelationExistException("The provider is already assigned to that department");
                    
                await _providerDepartmentRepository.UpdateAsync(oldProviderDepartment, newProviderDepartment);

                return oldProviderDepartment.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}