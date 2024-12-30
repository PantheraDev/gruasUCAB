using UserMs.Domain.Entities;

namespace UserMs.Application.Dtos.Users.Request{
    public class CreateUsersDto
    {
        public UserEmail? UserEmail { get; set; }
        public UserPassword? UserPassword { get; set; }
        public string? UsersType { get; init; }
        public UserProvider? UserProvider { get; set; }
        public UserDepartament? UserDepartament { get; set; }
    }
}