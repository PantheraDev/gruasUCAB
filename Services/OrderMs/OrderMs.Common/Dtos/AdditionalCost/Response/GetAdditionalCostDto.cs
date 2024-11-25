
namespace OrderMs.Common.Dtos.Response
{
    public class GetAdditionalCostDto
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string? CreatedBy { get; set; }

        public GetAdditionalCostDto(Guid id, string? createdBy, decimal value, string description)
        {
            Id = id;
            CreatedBy = createdBy;
            Value = value;
            Description = description;
        }

    }
}