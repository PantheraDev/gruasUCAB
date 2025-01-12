using UserMs.Domain.Entities;

namespace UserMs.Application.Dtos.Users.Request
{
    public class CreateUsersDto
    {
        public string UserEmail { get; set; } = String.Empty;
        public string? UsersType { get; init; }
        public Guid ProviderDepartmentId { get; set; }
    }
}