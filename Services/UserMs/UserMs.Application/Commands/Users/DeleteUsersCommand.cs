using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Commands.User
{
    public class DeleteUsersCommand : IRequest<UserId>
    {
        public UserId UserId { get; set; }
        public DeleteUsersCommand(UserId userId)
        {
            UserId = userId;
        }
    }
}