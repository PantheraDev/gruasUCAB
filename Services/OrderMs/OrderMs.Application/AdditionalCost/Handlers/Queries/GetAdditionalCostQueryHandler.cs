using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Queries;
using OrderMs.Common.Dtos.Response;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Handlers.Queries
{
    public class GetAdditionalCostQueryHandler : IRequestHandler<GetAdditionalCostQuery, GetAdditionalCostDto>
    {
        public IAdditionalCostRepository _additionalCostRepository;

        public GetAdditionalCostQueryHandler(IAdditionalCostRepository additionalCostRepository)
        {
            _additionalCostRepository = additionalCostRepository;
        }

        public async Task<GetAdditionalCostDto> Handle(GetAdditionalCostQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("AdditionalCost id is required");
            var additionalCostId = AdditionalCostId.Create(request.Id);
            var additionalCost = await _additionalCostRepository.GetByIdAsync(additionalCostId!);

            if (additionalCost == null || additionalCost.IsDeleted) throw new AdditionalCostNotFoundException("AdditionalCost not found");

            return new GetAdditionalCostDto(
                    additionalCost.Id.Value,
                    additionalCost.CreatedBy,
                    additionalCost.Value.Value,
                    additionalCost.Description.Value,
                    additionalCost.OrderId.Value,
                    (additionalCost.Verified == 0) ? "Verificado" : "NoVerificado"
                );
        }
    }
}