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
    public class AddTowToOrderCommandHandler : IRequestHandler<AddTowToOrderCommand, Guid>
    {
        public readonly IOrderRepository _orderRepository;
        public readonly IProviderService _providerService;
        public AddTowToOrderCommandHandler(IOrderRepository orderRepository, IProviderService providerService)
        {
            _orderRepository = orderRepository;
            _providerService = providerService;
        }

        public async Task<Guid> Handle(AddTowToOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tow = await _providerService.GetTowByIdAsync(TowId.Create(request.TowId)!);
                if (tow == null) throw new OrderNotFoundException($"Tow with id {request.TowId} not found");

                var order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId)!);
                if (order == null) throw new OrderNotFoundException($"Order with id {request.OrderId} not found");

                order = Order.Update(order,null,null,null,null,null,null,null,null,OrderOriginLocation.Create(tow.towLocation));
                order = Order.Update(order,null,null,null,null,null,null,null,TowId.Create(request.TowId),null);
                order = Order.Update(order,null,null,null,OrderState.Accepted,null,null,null,null,null);

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