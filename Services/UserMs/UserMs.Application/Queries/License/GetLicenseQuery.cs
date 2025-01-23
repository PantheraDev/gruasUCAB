using UserMs.Application.Dtos.License.Response;
using MediatR;

namespace UserMs.Application.Queries.License
{
    public class GetLicenseQuery : IRequest<List<GetLicenseDto>>
    {
        public Guid licenseId { get; set; }
        public DateTime? licenseDateExpiration { get; set; }
        public string? licenseNumber { get; set; }
    }
}