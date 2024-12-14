using UserMs.Application.Commands.User;
using UserMs.Application.Dtos.Users.Request;
using UserMs.Application.Dtos.Users.Response;
using UserMs.Domain.Entities;

namespace UserMs.Test.Data.MockData.User
{
    public static class BuildDataContextFaker
    {
        public static CreateUsersCommand BuildCreateUsersCommand()
        {
            var users = new CreateUsersDto()
            {
                UserId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail = UserEmail.Create("test@gmail.com"),
                UserPassword = UserPassword.Create("contraseña"),
                UsersType = "Operador"
            };
            return new CreateUsersCommand(users);
        }

        public static DeleteUsersCommand BuildDeleteUsersCommand()
        {
            var usersId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            return new DeleteUsersCommand(usersId);
        }

        public static UpdateUsersCommand BuildUpdateUsersCommand()
        {
            var usersId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var users = new UpdateUsersDto()
            {
                UserEmail = UserEmail.Create("test@gmail.com"),
                UserPassword = UserPassword.Create("contraseña"),
            };
            return new UpdateUsersCommand(usersId,users);
        }

        public static Users BuildCreateUsersEntity()
        {
            var users = new Users(
                UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail.Create("test@gmail.com"),
                UserPassword.Create("contraseña"),
                UsersType.Operador
                );
                
            return users;
        }

        public static List<Users> BuildCreateUsersEntityList()
        {
            var users1 = new Users(
                UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail.Create("test@gmail.com"),
                UserPassword.Create("contraseña"),
                UsersType.Operador
            );

            var users2 = new Users(
                UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209820")),
                UserEmail.Create("prueba@gmail.com"),
                UserPassword.Create("password"),
                UsersType.Proveedor
            );

            return new List<Users> { users1, users2 };
        }

        public static Users BuildUpdateUsersEntity()
        {
            var users = new Users(
                UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail.Create("juanPablo@gmail.com"),
                UserPassword.Create("665531"),
                UsersType.Administrador
            );

            return users;
        }

        public static CreateUsersDto GenerateCreateUsersDto()
        {
            return new CreateUsersDto()
            {
                UserId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail = UserEmail.Create("test@gmail.com"),
                UserPassword = UserPassword.Create("contraseña"),
            };
        }

        public static Task<List<GetUsersDto>> GenerateGetUsersDtoList()
        {
            var usersDto = new GetUsersDto()
            {
                UserId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"),
                UserEmail = "test@gmail.com",
                UserPassword = "contraseña",
            };

            return Task.FromResult(new List<GetUsersDto>() { usersDto });
        }

        public static Task<GetUsersDto> GenerateGetUsersDto()
        {
            var usersDto = new GetUsersDto()
            {
                UserId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"),
                UserEmail = "test@gmail.com",
                UserPassword = "contraseña",
            };

            return Task.FromResult(usersDto);
        }

        public static UpdateUsersDto GenerateUpdateUsersDto()
        {
            return new UpdateUsersDto()
            {
                UserEmail = UserEmail.Create("test@gmail.com"),
                UserPassword = UserPassword.Create("contraseña"),
            };
        }

        public static Task<Users> GenerateUsers()
        {
            var users = new Users(
                UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail.Create("test@gmail.com"),
                UserPassword.Create("contraseña"),
                UsersType.Administrador
            );

            return Task.FromResult(users);
        }

        public static Task<UserId> GetUserId()
        {
            var usersId = UserId.Create(Guid.NewGuid());

            return Task.FromResult(usersId);
        }
    }
}