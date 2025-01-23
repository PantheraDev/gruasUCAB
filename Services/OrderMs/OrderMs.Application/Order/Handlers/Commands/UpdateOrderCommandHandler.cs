using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Exceptions;
using OrderMs.Common.Primitives;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Guid>
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IIncidentRepository _incidentRepository;
        private readonly IPolicyRepository _policyRepository;
        private readonly IAdditionalCostRepository _additionalCostRepository;
        public UpdateOrderCommandHandler(IOrderRepository OrderRepository, IIncidentRepository incidentRepository, IPolicyRepository policyRepository, IAdditionalCostRepository additionalCostRepository)
        {
            _OrderRepository = OrderRepository ?? throw new ArgumentNullException(nameof(OrderRepository)); //*Valido que estas inyecciones sean exitosas
            _incidentRepository = incidentRepository ?? throw new ArgumentNullException(nameof(incidentRepository));
            _policyRepository = policyRepository ?? throw new ArgumentNullException(nameof(policyRepository));
            _additionalCostRepository = additionalCostRepository ?? throw new ArgumentNullException(nameof(additionalCostRepository));
        }

        public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldOrder = await _OrderRepository.GetByIdAsync(OrderId.Create(request.Id)!);

                if (oldOrder == null) throw new OrderNotFoundException("Order not found");


                if (request.Order.DestinyLocation != null)
                {
                    oldOrder = Order.Update(oldOrder, OrderDestinyLocation.Create(request.Order.DestinyLocation), null, null, null, null, null, null);
                }
                if (request.Order.TotalCost != null)
                {
                    oldOrder = Order.Update(oldOrder, null, OrderTotalCost.Create(request.Order.TotalCost.Value), null, null, null, null, null);
                }
                if (request.Order.Date != null)
                {
                    oldOrder = Order.Update(oldOrder, null, null, OrderDate.Create(request.Order.Date.Value.ToUniversalTime()), null, null, null, null);
                }
                if (request.Order.State != null)
                {
                    oldOrder = Order.Update(oldOrder, null, null, null, request.Order.State, null, null, null);
                }
                if (request.Order.IncidentId != null && await _incidentRepository.ExistsAsync(IncidentId.Create(request.Order.IncidentId.Value)!) is true)
                {
                    oldOrder = Order.Update(oldOrder, null, null, null, null, IncidentId.Create(request.Order.IncidentId.Value), null, null);
                }
                if (request.Order.PolicyId != null && await _policyRepository.ExistsAsync(PolicyId.Create(request.Order.PolicyId.Value)!) is true)
                {
                    oldOrder = Order.Update(oldOrder, null, null, null, null, null, PolicyId.Create(request.Order.PolicyId.Value), null);
                }
                if (request.Order.AdditionalCostId != null && await _additionalCostRepository.ExistsAsync(AdditionalCostId.Create(request.Order.AdditionalCostId.Value)!) is true)
                {
                    oldOrder = Order.Update(oldOrder, null, null, null, null, null, null, AdditionalCostId.Create(request.Order.AdditionalCostId.Value));
                }
                if (request.Order.TowId != null)
                {
                    oldOrder = Order.Update(oldOrder, null, null, null, null, null, null, null, TowId.Create(request.Order.TowId.Value));
                }

                //TODO: Hay que hacer que se guarde el UpdatedBy

                await _OrderRepository.UpdateAsync(oldOrder);

                return oldOrder.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}