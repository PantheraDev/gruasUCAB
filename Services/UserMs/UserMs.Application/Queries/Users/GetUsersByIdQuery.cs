using UserMs.Application.Dtos.Users.Response;
using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Queries.User
{
    public class GetUsersByIdQuery : IRequest<GetUsersDto>
    {
        public UserId Id { get; set; }

        public GetUsersByIdQuery(UserId id)
        {
            Id = id;
        }
    }
}