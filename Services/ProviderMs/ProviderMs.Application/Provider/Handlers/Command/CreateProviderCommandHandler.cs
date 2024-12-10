using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Application.Command;
using ProviderMs.Application.Validators;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers
{
    public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, Guid>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IDepartamentRepository _departamentRepository;
        private readonly ITowRepository _towRepository;

        public CreateProviderCommandHandler(IProviderRepository providerRepository, IDepartamentRepository departamentRepository, ITowRepository vehicleRepository)
        {
            _providerRepository = providerRepository;
            _departamentRepository = departamentRepository;
            _towRepository = vehicleRepository;
        }
        public async Task<Guid> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
        {
            try{
            var providerId = ProviderId.Create(Guid.NewGuid());
            var validator = new CreateProviderValidator();
            await validator.ValidateRequest(request.Provider);


            var departamentIds = new List<DepartamentId>();
            foreach (var departamentId in request.Provider.DepartamentId){
                var id = DepartamentId.Create(departamentId);
                if (await _departamentRepository.ExistsAsync(id!)is false){
                    throw new DepartamentNotFoundException(" Departament not found");
            }
            }

            var provider = new Provider(
                providerId,
                ProviderName.Create(request.Provider.Name),
                ProviderPhone.Create(request.Provider.PhoneNumber),
                ProviderEmail.Create(request.Provider.Email),
                ProviderRIF.Create(request.Provider.RIF),
                ProviderAddress.Create(request.Provider.Address)
            );
            if(request.Provider.DepartamentId != null)
            {
                provider.ProviderDepartaments = new List<Domain.Entities.ProviderDepartament>();
                foreach ( var departamentId in request.Provider.DepartamentId)
                {
                    var id = DepartamentId.Create(departamentId);
                    if(await _departamentRepository.ExistsAsync(id)is false)
                    {
                        throw new DepartamentNotFoundException("departament not found");
                    }
                    departamentIds.Add(id);
                }
            }
            
            await _providerRepository.AddAsync(provider);
            return provider.Id.Value;

            }
            catch(Exception ex){
                throw;
            }
        }
        
    }
}