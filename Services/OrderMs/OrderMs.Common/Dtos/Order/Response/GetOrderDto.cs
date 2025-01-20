
using OrderMS.Commons.Enums;

namespace OrderMs.Common.Dtos.Response
{
    public class GetOrderDto
    {
        public Guid Id { get; set; }
        public string DestinyLocation { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }
        //TODO: Posiblemente anadir los DTOS
        public Guid IncidentId { get; set; }
        public Guid PolicyId { get; set; }
        public List<GetAdditionalCostDto?> AdditionalCosts { get; set; }
        public Guid? TowId { get; set; }
        public string? CreatedBy { get; set; }

        public GetOrderDto(Guid id, string? createdBy, string destinyLocation, decimal totalCost, DateTime date, OrderState state, Guid incidentId, Guid policyId, Guid? towId = null, List<GetAdditionalCostDto?> additionalCosts = null)
        {
            Id = id;
            CreatedBy = createdBy;
            DestinyLocation = destinyLocation;
            TotalCost = totalCost;
            Date = date;
            State = state;
            IncidentId = incidentId;
            PolicyId = policyId;
            TowId = towId;
            AdditionalCosts = additionalCosts;
        }

    }
}