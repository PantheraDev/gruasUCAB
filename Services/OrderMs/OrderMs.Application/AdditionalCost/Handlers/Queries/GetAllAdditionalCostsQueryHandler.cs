using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Queries;
using OrderMs.ApplicationQueries;
using OrderMs.Common.Dtos.Request;
using OrderMs.Common.Dtos.Response;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Handlers.Queries
{
    public class GetAllAdditionalCostsQueryHandler : IRequestHandler<GetAllAdditionalCostsQuery, List<GetAdditionalCostDto>>
    {
        public IAdditionalCostRepository _additionalCostRepository;

        public GetAllAdditionalCostsQueryHandler(IAdditionalCostRepository additionalCostRepository)
        {
            _additionalCostRepository = additionalCostRepository;
        }

        public async Task<List<GetAdditionalCostDto>> Handle(GetAllAdditionalCostsQuery request, CancellationToken cancellationToken)
        {
            var AdditionalCost = await _additionalCostRepository.GetAllAsync();

            if (AdditionalCost == null) throw new AdditionalCostNotFoundException("AdditionalCosts are empty");

            return AdditionalCost.Where(c => !c.IsDeleted).Select(c =>
                new GetAdditionalCostDto(
                    c.Id.Value,
                    c.CreatedBy,
                    c.Value.Value,
                    c.Description.Value
                )
            ).ToList();
        }
    }
}