using MediatR;
using OrderMs.ApplicationQueries;
using OrderMs.Common.Dtos.Response;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;

namespace OrderMs.Application.Handlers.Queries
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<GetOrderDto>>
    {
        public IOrderRepository _OrderRepository;

        public GetAllOrdersQueryHandler(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        public async Task<List<GetOrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var Order = await _OrderRepository.GetAllAsync();

            if (Order == null) throw new OrderNotFoundException("Orders are empty");


            return Order.Where(c => !c.IsDeleted).Select(c =>
                new GetOrderDto(
                    c.Id.Value,
                    c.CreatedBy,
                    c.DestinyLocation.Value,
                    c.TotalCost.Value,
                    c.Date.Value,
                    c.State,
                    c.IncidentId.Value,
                    c.PolicyId.Value,
                    c.TowId?.Value,
                    c.AdditionalCosts.Select(x => new GetAdditionalCostDto(x.Id.Value, x.CreatedBy, x.Value.Value, x.Description.Value, x.OrderId.Value,(x.Verified == 0) ? "Verificado" : "NoVerificado")).ToList()!
                )
            ).ToList();
        }
    }
}