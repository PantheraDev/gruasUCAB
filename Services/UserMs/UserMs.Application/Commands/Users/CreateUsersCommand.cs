using UserMs.Application.Dtos.Users.Request;
using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Commands.User
{
    public class CreateUsersCommand : IRequest<UserId>
    {
        public CreateUsersDto Users { get; set; }

        public CreateUsersCommand(CreateUsersDto users)
        {
            Users = users;
        }
    }
}