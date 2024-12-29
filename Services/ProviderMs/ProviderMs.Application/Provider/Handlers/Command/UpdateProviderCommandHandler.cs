using MediatR;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    public class UpdateProviderCommandHandler : IRequestHandler<UpdateProviderCommand, Guid>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IDepartamentRepository _departamentRepository;
        private readonly ITowRepository _vehicleRepository; 
        public UpdateProviderCommandHandler(IProviderRepository providerRepository, IDepartamentRepository departamentRepository, ITowRepository vehicleRepository)
        {
            _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository)); //*Valido que estas inyecciones sean exitosas
            _departamentRepository = departamentRepository ?? throw new ArgumentNullException(nameof(departamentRepository));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        
        }

        public async Task<Guid> Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldProvider = await _providerRepository.GetByIdAsync(ProviderId.Create(request.Id)!);

                if (oldProvider == null) throw new ProviderNotFoundException("provider not found");


                if (request.Provider.Name != null)
                {
                    oldProvider = Provider.Update(oldProvider, ProviderName.Create(request.Provider.Name), null, null, null, null, null);
                }
                if (request.Provider.PhoneNumber != null)
                {
                    oldProvider = Provider.Update(oldProvider, null, ProviderPhone.Create(request.Provider.PhoneNumber), null, null, null,null);
                }
                if (request.Provider.Email != null)
                {
                    oldProvider = Provider.Update(oldProvider, null, null, ProviderEmail.Create(request.Provider.Email), null, null, null);
                }
                if (request.Provider.RIF != null)
                {
                    oldProvider = Provider.Update(oldProvider, null, null, null, ProviderRIF.Create(request.Provider.RIF), null, null);
                }
                if (request.Provider.Address != null)
                {
                    oldProvider = Provider.Update(oldProvider, null, null, null, null, ProviderAddress.Create(request.Provider.Address),null);
                }

                var departamentIds = new List<DepartamentId>();
                if (request.Provider.DepartamentId != null) //&& await _departamentRepository.ExistsAsync(DepartamentId.Create(request.Provider.DepartamentId.Value)!)is true)
                {
                    foreach(var departamentId in request.Provider.DepartamentId)
                    {
                        var id = DepartamentId.Create(departamentId);
                        if(await _departamentRepository.ExistsAsync(id)is false)
                        {
                            throw new DepartamentNotFoundException("departament not found");
                        }
                        departamentIds.Add(id);
                    }
                     oldProvider = Provider.Update(oldProvider, null, null, null, null, null,departamentIds);
                }

                await _providerRepository.UpdateAsync(oldProvider);

                return oldProvider.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}