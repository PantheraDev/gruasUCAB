using UserMs.Application.Dtos.Users.Request;
using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Commands.User
{
    public class UpdateUsersCommand : IRequest<Users>
    {
        public UserId UserId { get; set; }
        public UpdateUsersDto Users { get; set; }

        public UpdateUsersCommand(UserId userId, UpdateUsersDto users)
        {
            UserId = userId;
            Users = users;
        }
    }
}