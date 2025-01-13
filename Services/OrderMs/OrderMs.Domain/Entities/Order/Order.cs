using OrderMs.Common.Primitives;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;
using OrderMS.Commons.Enums;

namespace OrderMs.Domain.Entities
{
    //* Porque es sellada? Porque no necesito decir que esta clase tenga una modificacion externa y que todo su comportamiento este dentro de ella

    public sealed class Order : AggregateRoot
    {
        public OrderId Id { get; private set; }
        public OrderDestinyLocation DestinyLocation { get; private set; }
        public OrderOriginLocation? OriginLocation { get; set; }
        public OrderTotalCost TotalCost { get; private set; }
        public OrderDate Date { get; private set; }
        public OrderState State { get; private set; }
        public IncidentId IncidentId { get; private set; }
        public PolicyId PolicyId { get; private set; }
        public TowId? TowId { get; set; }
        public Incident? Incident { get; private set; }
        public Policy? Policy { get; private set; }
        public ICollection<AdditionalCost> AdditionalCosts { get; private set; } = new List<AdditionalCost>();

        public Order(OrderId id, OrderDestinyLocation destinyLocation, OrderTotalCost totalCost, OrderDate date, OrderState state, IncidentId incidentId, PolicyId policyId, TowId? towId = null)
        {
            Id = id;
            DestinyLocation = destinyLocation;
            TotalCost = totalCost;
            Date = date;
            State = state;
            IncidentId = incidentId;
            PolicyId = policyId;
            // AdditionalCostId = additionalCostId;
            TowId = towId;
        }

        public Order() { }

        public void AddAdditionalCost(AdditionalCost additionalCost)
        {
            AdditionalCosts.Add(additionalCost);
        }

        public static Order Update(Order Order, OrderDestinyLocation? destinyLocation, OrderTotalCost? totalCost, OrderDate? date, OrderState? state, IncidentId? incidentId, PolicyId? policyId, AdditionalCostId? additionalCostId = null, TowId? towId = null, OrderOriginLocation? originLocation = null)
        {
            // TODO: Esto podria solucionarse haciendo un DTO
            var updates = new List<Action>{
                    () => { if (destinyLocation != null) Order.DestinyLocation = destinyLocation; },
                    () => { if (totalCost != null) Order.TotalCost = totalCost; },
                    () => { if (date != null) Order.Date = date; },
                    () => { if (state != null) Order.State = state.Value; },
                    () => { if (incidentId != null) Order.IncidentId = incidentId; },
                    () => { if (policyId != null) Order.PolicyId = policyId; },
                    () => { if (towId != null) Order.TowId = towId; },
                    () => { if (originLocation != null) Order.OriginLocation = originLocation; }
                };

            updates.ForEach(update => update());
            return Order;
        }
    }
}