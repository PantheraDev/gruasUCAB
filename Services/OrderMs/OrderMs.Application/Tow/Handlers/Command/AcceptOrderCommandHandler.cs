using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Tow.Commands;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Core.Services;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;
using OrderMS.Commons.Enums;

namespace OrderMs.Application.Tow.Handlers.Command
{
    public class AcceptOrderCommandHandler : IRequestHandler<AcceptOrderCommand, Guid>
    {
        public readonly IOrderRepository _orderRepository;
        public AcceptOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId)!);
                if (order == null) throw new OrderNotFoundException($"Order with id {request.OrderId} not found");
                if(order.State != OrderState.Accepted) throw new InvalidAttributeException($"Order with id {request.OrderId} is not in Accepted");

                order = Order.Update(order, null, null, null, OrderState.InProgress, null, null, null, null, null);

                await _orderRepository.UpdateAsync(order);
                return order.Id.Value;
            }
            catch
            {
                throw;
            }

        }
    }
}