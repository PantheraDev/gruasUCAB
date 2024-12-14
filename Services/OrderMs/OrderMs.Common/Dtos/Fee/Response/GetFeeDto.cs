
namespace OrderMs.Common.Dtos.Response
{
    public class GetFeeDto
    {
        public Guid Id { get; set; }
        public decimal BasePrice { get; set; }
        public int Radius { get; set; }
        public decimal PriceXKm { get; set; }
        public string? CreatedBy { get; set; }

        public GetFeeDto(Guid id, string? createdBy, decimal basePrice, int radius, decimal priceXKm)
        {
            Id = id;
            CreatedBy = createdBy;
            BasePrice = basePrice;
            Radius = radius;
            PriceXKm = priceXKm;
        }

    }
}