using MediatR;
using ProviderMs.Application.Command;
using ProviderMs.Application.Validators;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers
{
    public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, Guid>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ITowRepository _towRepository;

        public CreateProviderCommandHandler(IProviderRepository providerRepository, IDepartmentRepository departmentRepository, ITowRepository vehicleRepository)
        {
            _providerRepository = providerRepository;
            _departmentRepository = departmentRepository;
            _towRepository = vehicleRepository;
        }
        public async Task<Guid> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var providerId = ProviderId.Create(Guid.NewGuid());
                var validator = new CreateProviderValidator();
                await validator.ValidateRequest(request.Provider);

                var provider = new Provider(
                    providerId,
                    ProviderName.Create(request.Provider.Name),
                    ProviderPhone.Create(request.Provider.PhoneNumber),
                    ProviderEmail.Create(request.Provider.Email),
                    ProviderRIF.Create(request.Provider.RIF),
                    ProviderAddress.Create(request.Provider.Address)
                );

                await _providerRepository.AddAsync(provider);
                return provider.Id.Value;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}