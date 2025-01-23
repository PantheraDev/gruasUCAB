
using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Dto.Response;
using OrderMs.Common.Dtos.Provider;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;
using OrderMS.Commons.Enums;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IIncidentRepository _incidentRepository;
        private readonly IPolicyRepository _policyRepository;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IIncidentRepository incidentRepository, IPolicyRepository policyRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository)); //*Valido que estas inyecciones sean exitosas
            _incidentRepository = incidentRepository ?? throw new ArgumentNullException(nameof(incidentRepository));
            _policyRepository = policyRepository ?? throw new ArgumentNullException(nameof(policyRepository));
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var validator = new CreateOrderValidator();
                await validator.ValidateRequest(request.Order);

                //* Se crean los Value Objects
                var orderId = OrderId.Create();
                var orderDestinyLocation = OrderDestinyLocation.Create(request.Order.DestinyLocation);
                var orderTotalCost = OrderTotalCost.Create(0);
                var orderDate = OrderDate.Create(DateTime.Now.ToUniversalTime());
                var orderIncidentId = IncidentId.Create(request.Order.IncidentId);
                if (await _incidentRepository.ExistsAsync(orderIncidentId!) is false)
                {
                    throw new IncidentNotFoundException("Incident not found");
                }
                var orderPolicyId = PolicyId.Create(request.Order.PolicyId);
                if (await _policyRepository.ExistsAsync(orderPolicyId!) is false)
                {
                    throw new PolicyNotFoundException("Policy not found");
                }
                var orderState = OrderState.ToAssign;

                //* Se crea el Orden
                var order = new Order(orderId, orderDestinyLocation, orderTotalCost, orderDate, orderState, orderIncidentId!, orderPolicyId!);

                //* Se agrega el Orden a la BD
                await _orderRepository.AddAsync(order);

                //* Retorna la id del Orden
                return order.Id.Value;
            }
            catch
            {
                throw;
            }
        }
    }
}