using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Dtos.Request;
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

        public AdditionalCost(AdditionalCostId id, AdditionalCostDescription description,
AdditionalCostValue value)
        {
            Id = id;
            Description = description;
            Value = value;
        }

        public AdditionalCost() { }

        public static AdditionalCost Update(AdditionalCost AdditionalCost, AdditionalCostDescription? description,
AdditionalCostValue? value)
        {
            var updates = new List<Action>{
                    () => { if (description != null) AdditionalCost.Description = description; },
                    () => { if (value != null) AdditionalCost.Value = value; },
                };

            updates.ForEach(update => update());
            return AdditionalCost;
        }
    }
}