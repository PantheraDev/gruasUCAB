using MediatR;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class DeleteProviderDepartamentCommandHandler : IRequestHandler<DeleteProviderDepartamentCommand, Guid>
    {
        private readonly IProviderDepartamentRepository _providerDepartamentRepository;
        public DeleteProviderDepartamentCommandHandler(IProviderDepartamentRepository providerDepartamentRepository)
        {
            _providerDepartamentRepository = providerDepartamentRepository ?? throw new ArgumentNullException(nameof(providerDepartamentRepository)); //*Valido que estas inyecciones sean exitosas  
        }

        public async Task<Guid> Handle(DeleteProviderDepartamentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var providerId = ProviderId.Create(request.ProviderId);
                var departamentId = DepartamentId.Create(request.DepartamentId);
                await _providerDepartamentRepository.DeleteAsync(providerId!,departamentId!);
                return departamentId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}