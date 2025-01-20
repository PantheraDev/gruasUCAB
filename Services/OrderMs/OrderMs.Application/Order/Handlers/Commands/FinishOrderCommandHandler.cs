
using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Core.Services;
using OrderMs.Domain.Entities;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;
using OrderMS.Commons.Enums;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class FinishOrderCommandHandler : IRequestHandler<FinishOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IIncidentRepository _incidentRepository;
        private readonly IPolicyRepository _policyRepository;
        private readonly IAdditionalCostRepository _additionalCostRepository;
        private readonly IFeeRepository _feeRepository;
        private readonly IProviderService _providerService;
        private readonly IGoogleApiService _googleApiService;
        public FinishOrderCommandHandler(IOrderRepository orderRepository, IIncidentRepository incidentRepository, IPolicyRepository policyRepository, IAdditionalCostRepository additionalCostRepository, IFeeRepository feeRepository, IProviderService providerService, IGoogleApiService googleApiService)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository)); //*Valido que estas inyecciones sean exitosas
            _incidentRepository = incidentRepository ?? throw new ArgumentNullException(nameof(incidentRepository));
            _policyRepository = policyRepository ?? throw new ArgumentNullException(nameof(policyRepository));
            _additionalCostRepository = additionalCostRepository ?? throw new ArgumentNullException(nameof(additionalCostRepository));
            _feeRepository = feeRepository;
            _providerService = providerService;
            _googleApiService = googleApiService;
        }

        public async Task<Guid> Handle(FinishOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId)!);
                if (order == null) throw new OrderNotFoundException("Order not found");
                if (order.State == OrderState.Finished) throw new InvalidAttributeException("Order already finished");
                if (order.State != OrderState.InProgress) throw new InvalidAttributeException("Order is not in progress");

                var incident = await _incidentRepository.GetByIdAsync(order.IncidentId);
                var policy = await _policyRepository.GetByIdAsync(order.PolicyId);
                var fee = await _feeRepository.GetByIdAsync(policy!.FeeId);

                var originToIncident = await _googleApiService.GetDistanceCompleteRoute(order.OriginLocation!.Value, incident!.DestinyLocation.Value);
                var incidentToDestiny = await _googleApiService.GetDistanceCompleteRoute(incident.DestinyLocation.Value, order.DestinyLocation!.Value);
                
                decimal totalDistance = (decimal)((originToIncident.DistanceValue + incidentToDestiny.DistanceValue) * 0.001);
                totalDistance = Math.Round(totalDistance, 2);
                decimal totalCost = (((totalDistance - fee!.Radius.Value) * fee!.PriceXKm.Value) + fee!.BasePrice.Value) - policy.Coverage.Value;
                if(totalCost < 0)
                {
                    totalCost = 0;
                }
                decimal addCost = 0;
                foreach (var orden in order.AdditionalCosts)
                {
                    addCost = +Convert.ToDecimal(orden.Value.Value);
                }
                OrderTotalCost costoFinal = OrderTotalCost.Create(totalCost + addCost);
                
                order = Order.Update(order, null, costoFinal, null, OrderState.Finished, null, null);

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