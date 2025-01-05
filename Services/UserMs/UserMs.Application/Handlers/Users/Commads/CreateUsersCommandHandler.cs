using UserMs.Application.Commands.User;
using UserMs.Application.Validators;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Infrastructure.Exceptions;
using UserMs.Core.Interface;

namespace UserMs.Application.Handlers.User.Commands
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand, UserId>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IAuthMsService _authMsService;

        public CreateUsersCommandHandler(IUsersRepository usersRepository, IAuthMsService authMsService)
        {
            _usersRepository = usersRepository;
            _authMsService = authMsService;
        }



        public async Task<UserId> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateUsersValidator();
                await validator.ValidateRequest(request.Users);
                Func<string> generatePassword = () =>
                                {
                                    const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
                                    var random = new Random();
                                    return new string(Enumerable.Repeat(validChars, 8)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
                                };
                var usersEmailValue = request.Users.UserEmail?.Value;
                var usersPasswordValue = generatePassword();
                var usersProviderValue = request.Users.UserProvider?.Value;
                var usersDepartamentValue = request.Users.UserDepartament?.Value;

                await _authMsService.CreateUserAsync(usersEmailValue!, usersPasswordValue);
                var userId = await _authMsService.GetUserByUserName(UserEmail.Create(usersEmailValue!));
                await _authMsService.AssignClientRoleToUser(userId, request.Users.UsersType!);

                var users = new Users(
                    UserId.Create(userId),
                    UserEmail.Create(usersEmailValue ?? string.Empty),
                    UserPassword.Create(usersPasswordValue ?? string.Empty),
                    UserProvider.Create(usersProviderValue.Value),
                    UserDepartament.Create(usersDepartamentValue.Value),
                    Enum.Parse<UsersType>(request.Users.UsersType!)
                );

                if (request.Users.UsersType == null)
                {
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