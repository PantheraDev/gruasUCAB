using UserMs.Domain.Entities;

namespace UserMs.Application.Dtos.Users.Request{
    public class CreateUsersDto
    {
        public UserEmail? UserEmail { get; set; }
        public UserPassword? UserPassword { get; set; }
        public string? UsersType { get; init; }
    }
}