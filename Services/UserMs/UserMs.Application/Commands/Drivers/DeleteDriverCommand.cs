using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Commands.Drivers
{
    public class DeleteDriverCommand : IRequest<UserId>
    {
        public UserId UserId { get; set; }
        public DeleteDriverCommand(UserId userId)
        {
            UserId = userId;
        }
    }
}