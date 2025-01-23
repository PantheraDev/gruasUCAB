using MediatR;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class DeleteProviderCommandHandler : IRequestHandler<DeleteProviderCommand, Guid>
    {
        private readonly IProviderRepository _providerRepository;
        public DeleteProviderCommandHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository)); //*Valido que estas inyecciones sean exitosas
            
        }

        public async Task<Guid> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var providerId = ProviderId.Create(request.ProviderId);
                await _providerRepository.DeleteAsync(providerId!);
                return providerId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}