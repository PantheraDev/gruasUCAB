using MediatR;
using ProviderMs.Application.Command;
using ProviderMs.Application.Validators;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers
{
    public class CreateProviderDepartmentCommandHandler : IRequestHandler<CreateProviderDepartmentCommand, Guid>
    {
        private readonly IProviderDepartmentRepository _providerDepartmentRepository;

        public CreateProviderDepartmentCommandHandler(IProviderDepartmentRepository providerDepartmentRepository)
        {
            _providerDepartmentRepository = providerDepartmentRepository;
        }
        public async Task<Guid> Handle(CreateProviderDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var providerDepartmentId = ProviderDepartmentId.Create(Guid.NewGuid());
                var validator = new CreateProviderDepartmentValidator();
                await validator.ValidateRequest(request.ProviderDepartment);

                if (await _providerDepartmentRepository.RelationExistAsync(
                    ProviderId.Create(request.ProviderDepartment.ProviderId)!,
                    DepartmentId.Create(request.ProviderDepartment.DepartmentId)!
                )) throw new ProviderDepartmentRelationExistException("The provider is already assigned to that department");

                var providerDepartment = new ProviderDepartment(
                    providerDepartmentId!,
                    ProviderId.Create(request.ProviderDepartment.ProviderId)!,
                    DepartmentId.Create(request.ProviderDepartment.DepartmentId)!
                );

                await _providerDepartmentRepository.AddAsync(providerDepartment);
                return providerDepartment.Id.Value;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}