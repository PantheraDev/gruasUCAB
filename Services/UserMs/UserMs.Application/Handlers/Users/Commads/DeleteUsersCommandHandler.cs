using UserMs.Application.Commands.User;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Core.Interface;

namespace UserMs.Application.Handlers.User.Commands
{
    public class DeleteUsersCommandHandler : IRequestHandler<DeleteUsersCommand, UserId>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IAuthMsService _authMsService;

        public DeleteUsersCommandHandler(IUsersRepository usersRepository, IAuthMsService authMsService)
        {
            _usersRepository = usersRepository;
            _authMsService = authMsService;
        }

        public async Task<UserId> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _usersRepository.GetUsersById(request.UserId);
                var deleted = await _authMsService.DeleteUserAsync(users!.UserId);
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