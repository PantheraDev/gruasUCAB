using UserMs.Application.Dtos.License.Response;
using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Queries.License
{
    public class GetLicenseByIdQuery : IRequest<GetLicenseDto>
    {
        public LicenseId Id { get; set; }

        public GetLicenseByIdQuery(LicenseId id)
        {
            Id = id;
        }
    }
}