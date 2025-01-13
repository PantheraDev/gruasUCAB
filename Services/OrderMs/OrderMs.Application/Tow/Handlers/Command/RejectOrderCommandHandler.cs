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
    public class RejectOrderCommandHandler : IRequestHandler<RejectOrderCommand, Guid>
    {
        public readonly IOrderRepository _orderRepository;
        public RejectOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(RejectOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId)!);
                if (order == null) throw new OrderNotFoundException($"Order with id {request.OrderId} not found");
                if (order.State != OrderState.Accepted) throw new InvalidAttributeException($"Order with id {request.OrderId} is not in Accepted");

                order.OriginLocation = null;
                order.TowId = null;
                order = Order.Update(order, null, null, null, OrderState.ToAssign, null, null, null, null, null);

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