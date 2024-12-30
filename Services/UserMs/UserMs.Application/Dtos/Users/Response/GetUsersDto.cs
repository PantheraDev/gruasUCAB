namespace UserMs.Application.Dtos.Users.Response{
    public class GetUsersDto
    {
        public Guid UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? UsersType { get; set; }
        public Guid? UserProvider { get; set; }
        public Guid? UserDepartament { get; set; }
    }
}