using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Dtos.Request;
using OrderMs.Common.Enums;
using OrderMs.Common.Primitives;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Domain.Entities
{
    //* Porque es sellada? Porque no necesito decir que esta clase tenga una modificacion externa y que todo su comportamiento este dentro de ella

    public sealed class AdditionalCost : AggregateRoot
    {
        public AdditionalCostId Id { get; private set; }
        public AdditionalCostDescription Description { get; private set; }
        public AdditionalCostValue Value { get; private set; }
        public AdditionalCostVerified Verified { get; private set; } = AdditionalCostVerified.NoVerificado;
        public OrderId OrderId { get; private set; } // Referencia a la orden
        public Order Order { get; private set; } // Navegación hacia Order

        public AdditionalCost(AdditionalCostId id, AdditionalCostDescription description,
AdditionalCostValue value, OrderId orderId)
        {
            Id = id;
            Description = description;
            Value = value;
            OrderId = orderId;
        }

        public AdditionalCost() { }

        public static AdditionalCost Update(AdditionalCost AdditionalCost, AdditionalCostDescription? description,
AdditionalCostValue? value, AdditionalCostVerified? verified)
        {
            var updates = new List<Action>{
                    () => { if (description != null) AdditionalCost.Description = description; },
                    () => { if (value != null) AdditionalCost.Value = value; },
                    () => { if (verified != null) AdditionalCost.Verified = verified.Value; },
                };

            updates.ForEach(update => update());
            return AdditionalCost;
        }
    }
}