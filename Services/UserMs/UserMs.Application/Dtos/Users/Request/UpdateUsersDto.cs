using UserMs.Domain.Entities;

namespace UserMs.Application.Dtos.Users.Request{
    public class UpdateUsersDto
    {
        public UserEmail? UserEmail { get; set; }
        public UserPassword? UserPassword { get; set; }
        public UsersType UsersType { get; set; }
        public UserProvider? UserProvider { get; set; }
        public UserDepartament? UserDepartament { get; set; }
    }
}