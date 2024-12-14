using UserMs.Application.Dtos.Drivers.Response;
using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Queries.Drivers
{
    public class GetDriverByIdQuery : IRequest<GetDriverDto>
    {
        public UserId Id { get; set; }

        public GetDriverByIdQuery(UserId id)
        {
            Id = id;
        }
    }
}