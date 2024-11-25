using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Primitives;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Guid>
    {
        private readonly IOrderRepository _OrderRepository;
        public DeleteOrderCommandHandler(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository ?? throw new ArgumentNullException(nameof(OrderRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var orderId = OrderId.Create(request.OrderId);
                await _OrderRepository.DeleteAsync(orderId!);
                return orderId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}