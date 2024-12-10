using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.ApplicationQueries;
using ProviderMs.Common.dto.Request;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetAllProvidersQueryHandler : IRequestHandler<GetAllProvidersQuery, List<GetProvider>>
    {
        public IProviderRepository _providerRepository;

        public GetAllProvidersQueryHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<List<GetProvider>> Handle(GetAllProvidersQuery request, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.GetAllAsync();

            if (provider == null) throw new ProviderNotFoundException("Providers are empty");

            return provider.Where(p => !p.IsDeleted).Select(p =>
                new GetProvider(
                    p.Id.Value,
                    p.Name.Value,
                    p.Phone.Value,
                    p.Email.Value,
                    p.RIF.Value,
                    p.Address.Value,
                    p.ProviderDepartaments?.Select(pd => pd.DepartamentId.Value).ToList() ?? new List<Guid>(),
                    p.CreatedBy ?? string.Empty
                )
            ).ToList();
        }
    }
}