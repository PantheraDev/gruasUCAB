using UserMs.Application.Commands.User;
using UserMs.Application.Validators;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Application.Handlers.User.Commands
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand, UserId>
    {
        private readonly IUsersRepository _usersRepository;

        public CreateUsersCommandHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UserId> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            try{
                var validator = new CreateUsersValidator();
                await validator.ValidateRequest(request.Users);

                var usersEmailValue = request.Users.UserEmail?.Value;
                var usersPasswordValue = request.Users.UserPassword?.Value;

                var users = new Users(
                    UserId.Create(),
                    UserEmail.Create(usersEmailValue ?? string.Empty),
                    UserPassword.Create(usersPasswordValue ?? string.Empty),
                    Enum.Parse<UsersType>(request.Users.UsersType!)
                );

                if(request.Users.UsersType == null){
                    throw new NullAtributeException("UsersType can't be null");
                }

                await _usersRepository.AddAsync(users);

                return users.UserId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}