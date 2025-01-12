
namespace UserMs.Application.Dtos.Users.Request{
    public class UpdateUsersDto
    {
        public string? UserEmail { get; set; } = String.Empty;
        public string? UserPassword { get; set; } = String.Empty;
        public UsersType UsersType { get; init; }
        public Guid? ProviderDepartmentId { get; set; }
    }
}