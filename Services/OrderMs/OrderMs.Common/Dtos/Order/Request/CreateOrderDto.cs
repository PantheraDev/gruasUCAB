using OrderMS.Commons.Enums;

namespace OrderMs.Common.Dtos.Request
{
    public record CreateOrderDto
    {
        public string DestinyLocation { get; init; } = string.Empty;
        public Guid IncidentId { get; init; }
        public Guid PolicyId { get; init; }

    }
}