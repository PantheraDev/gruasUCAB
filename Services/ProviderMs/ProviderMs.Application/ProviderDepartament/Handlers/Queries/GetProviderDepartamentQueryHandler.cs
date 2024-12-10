using MediatR;
using ProviderMs.Application.Queries;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetProviderDepartamentQueryHandler : IRequestHandler<GetProviderDepartamentQuery, List<GetProviderDepartamentByProviderId>>
    {
        public IProviderDepartamentRepository _providerDepartamentRepository;

        public GetProviderDepartamentQueryHandler(IProviderDepartamentRepository providerDepartamentRepository)
        {
            _providerDepartamentRepository = providerDepartamentRepository;
        }

        public async Task<List<GetProviderDepartamentByProviderId>> Handle(GetProviderDepartamentQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("Provider id is required");
            var providerId = ProviderId.Create(request.Id);

            var providerDepartaments = await _providerDepartamentRepository.GetByProviderIdAsync(providerId!);

            // Filtrar directamente en la proyección con Where
            var result = providerDepartaments
                .Where(pd => !pd.IsDeleted) // Filtra donde IsDeleted es false
                .Select(pd => new GetProviderDepartamentByProviderId(pd.DepartamentId.Value))
                .ToList();

            // Manejo de lista vacía (opcional, pero recomendado)
            if (!result.Any())
            {
                return new(); // Retorna una lista vacía
                // Opcionalmente, puedes lanzar una excepción:
                // throw new ProviderNotFoundException("No se encontraron departamentos activos para el proveedor especificado.");
            }

            return result;
        }
    }
}