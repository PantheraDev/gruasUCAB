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
    public class GetFeeQueryHandler : IRequestHandler<GetFeeQuery, GetFeeDto>
    {
        public IFeeRepository _FeeRepository;

        public GetFeeQueryHandler(IFeeRepository FeeRepository)
        {
            _FeeRepository = FeeRepository;
        }

        public async Task<GetFeeDto> Handle(GetFeeQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("Fee id is required");
            var feeId = FeeId.Create(request.Id);
            var fee = await _FeeRepository.GetByIdAsync(feeId!);

            if (fee == null || fee.IsDeleted) throw new FeeNotFoundException("Fee not found");

            return new GetFeeDto(
                fee.Id.Value,
                fee.CreatedBy,
                fee.BasePrice.Value,
                fee.Radius.Value,
                fee.PriceXKm.Value
            );
        }
    }
}