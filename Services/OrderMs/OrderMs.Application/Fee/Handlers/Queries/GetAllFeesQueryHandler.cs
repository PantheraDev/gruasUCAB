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
    public class GetAllFeesQueryHandler : IRequestHandler<GetAllFeesQuery, List<GetFeeDto>>
    {
        public IFeeRepository _FeeRepository;

        public GetAllFeesQueryHandler(IFeeRepository FeeRepository)
        {
            _FeeRepository = FeeRepository;
        }

        public async Task<List<GetFeeDto>> Handle(GetAllFeesQuery request, CancellationToken cancellationToken)
        {
            var fee = await _FeeRepository.GetAllAsync();

            if (fee == null) throw new FeeNotFoundException("Fees are empty");

            return fee.Where(c => !c.IsDeleted).Select(c =>
                new GetFeeDto(
                    c.Id.Value,
                    c.CreatedBy,
                    c.BasePrice.Value,
                    c.Radius.Value,
                    c.PriceXKm.Value
                )
            ).ToList();
        }
    }
}