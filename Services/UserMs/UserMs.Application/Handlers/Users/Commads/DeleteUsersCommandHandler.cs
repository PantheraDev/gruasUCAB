using UserMs.Application.Commands.User;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;

namespace UserMs.Application.Handlers.User.Commands
{
    public class DeleteUsersCommandHandler : IRequestHandler<DeleteUsersCommand, UserId>
    {
        private readonly IUsersRepository _usersRepository;

        public DeleteUsersCommandHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UserId> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _usersRepository.GetUsersById(request.UserId);
                users.SetUserDelete(UserDelete.Create(true));
                await _usersRepository.DeleteUsersAsync(request.UserId);
                return request.UserId;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}