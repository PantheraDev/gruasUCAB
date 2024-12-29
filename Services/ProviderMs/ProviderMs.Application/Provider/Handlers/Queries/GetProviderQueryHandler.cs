using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Application.Queries;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using ProviderMs.Core.Database;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetProviderQueryHandler : IRequestHandler<GetProviderQuery, GetProvider>
    {
        public IProviderRepository _providerRepository;
         private readonly IApplicationDbContext _dbContext;

        public GetProviderQueryHandler(IProviderRepository providerRepository, IApplicationDbContext dbContext)
        {
            _providerRepository = providerRepository;
            _dbContext = dbContext;
        }

        public async Task<GetProvider> Handle(GetProviderQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("Provider id is required");
            var providerId = ProviderId.Create(request.Id);
            var provider = await _providerRepository.GetByIdAsync(providerId!/*, include: p => p.Include(pd => pd.Departament)*/);

            /*if (provider == null || provider.IsDeleted) throw new ProviderNotFoundException("Provider not found");
                provider.ProviderDepartaments = await _dbContext.providerDepartaments
                 .Where(pd => pd.ProviderId == providerId)
                .ToListAsync();*/
            var departamentIds = provider.ProviderDepartaments?.Select(pd => pd.DepartamentId.Value).ToList()?? new List<Guid>(); 
            var createdBy = provider.CreatedBy ?? string.Empty;

            return new GetProvider(
                provider.Id.Value,
                provider.Name.Value,
                provider.Phone.Value,
                provider.Email.Value,
                provider.RIF.Value,
                provider.Address.Value,
                //provider.ProviderDepartaments?.Select(pd => new ProviderDepartamentDto(pd.Departament.Id.Value, pd.Departament.Name.Value)).ToList()?? new List<ProviderDepartamentDto>(),
                departamentIds,
                createdBy
            );
        }
    }
}