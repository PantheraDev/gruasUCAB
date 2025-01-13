using MediatR;
using OrderMs.ApplicationQueries;
using OrderMs.Common.Dto.Response;
using OrderMs.Common.Dtos.Provider;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Core.Services;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;
using OrderMS.Commons.Enums;

namespace OrderMs.Application.Handlers.Queries
{
    public class GetTowsAvailableQueryHandler : IRequestHandler<GetTowsAvailableQuery, List<TowsAvaliable>>
    {
        public IProviderService _ProviderService;
        public IGoogleApiService _googleApiService;
        public IOrderRepository _orderRepository;
        public IIncidentRepository _incidentRepository;
        public IPolicyRepository _policyRepository;
        public IInsuredVehicleRepository _insuredVehicleRepository;

        public GetTowsAvailableQueryHandler(IProviderService ProviderService, IGoogleApiService googleApiService, IOrderRepository orderRepository, IIncidentRepository incidentRepository, IPolicyRepository policyRepository, IInsuredVehicleRepository insuredVehicleRepository)
        {
            _ProviderService = ProviderService;
            _googleApiService = googleApiService;
            _orderRepository = orderRepository;
            _incidentRepository = incidentRepository;
            _policyRepository = policyRepository;
            _insuredVehicleRepository = insuredVehicleRepository;
        }

        public async Task<List<TowsAvaliable>> Handle(GetTowsAvailableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.orderId)!);
                if (order == null) throw new OrderNotFoundException("Order not found");

                var orders = await _orderRepository.GetAllAsync();
                Console.WriteLine("This:" + orders.Count);

                Incident? incident = await _incidentRepository.GetByIdAsync(IncidentId.Create(order.IncidentId.Value)!);
                if (incident == null) throw new IncidentNotFoundException("Incident not found");

                Policy? policy = await _policyRepository.GetByIdAsync(PolicyId.Create(order.PolicyId.Value)!);
                InsuredVehicle? insuredVehicle = await _insuredVehicleRepository.GetByIdAsync(InsuredVehicleId.Create(policy!.InsuredVehicleId.Value)!);


                List<GetProvider?> providers = await _ProviderService.GetProviderAsync();
                var provider = providers.FirstOrDefault(p => p!.name == "UCAB");

                List<GetTow?> Tows = await _ProviderService.GetTowsAsync();
                Console.WriteLine(insuredVehicle!.Weight.Value);
                if (insuredVehicle!.Weight.Value <= 3800)
                {
                    // Todas las grúas (small, medium, tall)
                    Tows = Tows.Where(tow => tow!.towAvailability == 0 && tow!.towDriver != null).ToList();
                    Console.WriteLine(Tows.Count);
                    if (Tows == null || Tows.Count == 0) throw new OrderNotFoundException("Tows are empty");
                }
                else if (insuredVehicle.Weight.Value > 3800 && insuredVehicle.Weight.Value <= 5800)
                {
                    // Solo grúas medium y tall
                    Tows = Tows.Where(tow => tow!.towAvailability == 0 && tow!.towDriver != null && (tow!.towType == 1 || tow!.towType == 2)).ToList();
                    Console.WriteLine("Medium: " + Tows.Count);
                    if (Tows == null || Tows.Count == 0) throw new OrderNotFoundException("Tows are empty");
                }
                else if (insuredVehicle.Weight.Value > 5800)
                {
                    // Solo grúas tall
                    Tows = Tows.Where(tow => tow!.towAvailability == 0 && tow!.towDriver != null && tow!.towType == 2).ToList();
                    Console.WriteLine("Tall:" + Tows.Count);
                    if (Tows == null || Tows.Count == 0) throw new OrderNotFoundException("Tows are empty");
                }

                Tows = Tows
                        .Where(tow => tow != null && !orders
                            .Where(order => order.TowId != null &&
                                            (order.State == OrderState.Accepted || order.State == OrderState.InProgress))
                            .Any(order => order.TowId!.Value == tow.id))
                        .ToList();
                if (Tows == null || Tows.Count == 0) throw new OrderNotFoundException("Tows are empty");

                List<TowsAvaliable> TowsAvaliable = await _googleApiService.GetDistanceAvailableVehiclesToOrigin(
                    Tows!, incident.DestinyLocation);

                TowsAvaliable = TowsAvaliable
                                .OrderBy(t => t.ProviderId != provider!.id) // Coloca primero el ProviderId específico
                                .ThenBy(t => t.ProviderId)                        // Ordena por ProviderId para el resto
                                .ThenBy(t => t.EtaValue)                          // Ordena por EtaValue secundariamente
                                .ToList();

                return TowsAvaliable!;
            }
            catch
            {
                throw;
            }
        }
    }
}