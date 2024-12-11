using UserMs.Application.Commands.User;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Infrastructure.Exceptions;
using UserMs.Core.Interface;

namespace UserMs.Application.Handlers.User.Commands
{
    public class UpdateUsersCommandHandler : IRequestHandler<UpdateUsersCommand,Users>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IAuthMsService _authMsService;

        public UpdateUsersCommandHandler(IUsersRepository usersRepository, IAuthMsService authMsService)
        {
            _usersRepository = usersRepository;
            _authMsService = authMsService;
        }

        public async Task<Users> Handle(UpdateUsersCommand request, CancellationToken cancellationToken)
        {
            var existingUsers = await _usersRepository.GetUsersById(request.UserId);

            if (existingUsers == null)
                throw new UserNotFoundException("User not found.");
            
            if (existingUsers.UserDelete.Value)
                throw new UserNotFoundException("User not found.");

            if (existingUsers != null)
            {
                if ((int)request.Users.UsersType < 0 || (int)request.Users.UsersType > 2)
                {
                    throw new NullAtributeException("UsersType must be between 0 and 2");
                }

                existingUsers.SetUserEmail(UserEmail.Create(request.Users.UserEmail.Value));
                existingUsers.SetUserPassword(UserPassword.Create(request.Users.UserPassword.Value));
                existingUsers.SetUsersType(request.Users.UsersType);
                existingUsers.SetUserProvider(UserProvider.Create(request.Users.UserProvider.Value));
                existingUsers.SetUserDepartament(UserDepartament.Create(request.Users.UserDepartament.Value));

                await _authMsService.UpdateUser(existingUsers.UserId, existingUsers);
                await _usersRepository.UpdateUsersAsync(request.UserId,existingUsers);
            }

            return existingUsers;
        }
    }
}