using OrderMS.Commons.Enums;

namespace OrderMs.Common.Dtos.Request
{
    public record CreateOrderDto
    {
        public string DestinyLocation { get; init; }
        public decimal TotalCost { get; init; }
        public DateTime Date { get; init; }
        public OrderState State { get; init; }
        public Guid IncidentId { get; init; }
        public Guid PolicyId { get; init; }
        public Guid? AdditionalCostId { get; init; }
        public Guid? TowId { get; init; }

    }
}