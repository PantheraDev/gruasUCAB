using UserMs.Application.Dtos.Drivers.Response;
using MediatR;

namespace UserMs.Application.Queries.Drivers
{
    public class GetDriverQuery : IRequest<List<GetDriverDto>>
    {
        public Guid userId { get; set; }
        public string? userEmail { get; set; }
        public string? userPassword { get; set; }
        public string? driverAvailable { get; set; }
        public Guid? driverLicenseId { get; set; }
    }
}