using OrderMs.Common.Primitives;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Domain.Entities
{
    //* Porque es sellada? Porque no necesito decir que esta clase tenga una modificacion externa y que todo su comportamiento este dentro de ella

    public sealed class Fee : AggregateRoot
    {
        public FeeId Id { get; private set; }
        public FeeBasePrice BasePrice { get; private set; }
        public FeeRadius Radius { get; private set; }
        public FeePriceXKm PriceXKm { get; private set; }

        public Fee(FeeId id, FeeBasePrice basePrice, FeeRadius radius, FeePriceXKm priceXKm)
        {
            Id = id;
            PriceXKm = priceXKm;
            BasePrice = basePrice;
            Radius = radius;
        }

        public Fee() { }

        public static Fee Update(Fee Fee, FeeBasePrice? basePrice, FeeRadius? radius, FeePriceXKm? priceXKm)
        {
            // TODO: Esto podria solucionarse haciendo un DTO
            var updates = new List<Action>{
                    () => { if (basePrice != null) Fee.BasePrice = basePrice; },
                    () => { if (radius != null) Fee.Radius = radius; },
                    () => { if (priceXKm != null) Fee.PriceXKm = priceXKm; }
                };

            updates.ForEach(update => update());
            return Fee;
        }
    }
}