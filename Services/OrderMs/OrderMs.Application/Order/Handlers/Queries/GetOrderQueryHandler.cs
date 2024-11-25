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
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GetOrderDto>
    {
        public IOrderRepository _OrderRepository;

        public GetOrderQueryHandler(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        public async Task<GetOrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("Order id is required");
            var orderId = OrderId.Create(request.Id);
            var Order = await _OrderRepository.GetByIdAsync(orderId!);

            if (Order == null || Order.IsDeleted) throw new OrderNotFoundException("Order not found");

            return new GetOrderDto(
                    Order.Id.Value,
                    Order.CreatedBy,
                    Order.DestinyLocation.Value,
                    Order.TotalCost.Value,
                    Order.Date.Value,
                    Order.State,
                    Order.IncidentId.Value,
                    Order.PolicyId.Value,
                    Order.AdditionalCostId?.Value,
                    Order.TowId?.Value
                );
        }
    }
}