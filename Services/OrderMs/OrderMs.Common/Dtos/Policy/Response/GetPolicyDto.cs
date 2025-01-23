
namespace OrderMs.Common.Dtos.Response
{
    public class GetPolicyDto
    {
        public Guid Id { get; set; }
        public decimal Coverage { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime IssueDate { get; set; }
        //TODO: Posiblemente hay que anadir el dto de InsuredVehicle y Fee
        public Guid InsuredVehicleId { get; set; }
        public Guid FeeId { get; set; }
        public string? CreatedBy { get; set; }

        public GetPolicyDto(Guid id, string? createdBy, decimal coverage, DateTime expirationDate, DateTime issueDate, Guid insuredVehicleId, Guid feeId)
        {
            Id = id;
            CreatedBy = createdBy;
            Coverage = coverage;
            ExpirationDate = expirationDate;
            IssueDate = issueDate;
            InsuredVehicleId = insuredVehicleId;
            FeeId = feeId;
        }

    }
}