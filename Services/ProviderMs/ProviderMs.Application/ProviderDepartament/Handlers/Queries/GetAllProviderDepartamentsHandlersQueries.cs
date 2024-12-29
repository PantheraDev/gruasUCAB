using MediatR;
using ProviderMs.ApplicationQueries;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetAllProviderDepartamentsQueryHandler : IRequestHandler<GetAllProviderDepartamentsQuery, List<GetProviderDepartament>>
    {
        public IProviderDepartamentRepository _providerDepartamentRepository;

        public GetAllProviderDepartamentsQueryHandler(IProviderDepartamentRepository providerDepartamentRepository)
        {
            _providerDepartamentRepository = providerDepartamentRepository;
        }

        public async Task<List<GetProviderDepartament>> Handle(GetAllProviderDepartamentsQuery request, CancellationToken cancellationToken)
        {
            var providerDepartament = await _providerDepartamentRepository.GetAllAsync();

            if (providerDepartament == null) throw new ProviderNotFoundException("ProviderDepartaments are empty");

            return providerDepartament.Where(p => !p.IsDeleted).Select(p =>
                new GetProviderDepartament(
                    p.ProviderId.Value,
                    p.DepartamentId.Value
                )
            ).ToList();
        }
    }
}