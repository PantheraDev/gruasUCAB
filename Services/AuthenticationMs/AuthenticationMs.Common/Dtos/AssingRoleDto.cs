namespace AuthenticationMs.Common.Dtos
{
    public class AssingRoleDto
    {
        public Guid UserId { get; set; }
        public string RoleName { get; set; } = String.Empty;
        public string ClientId { get; set; } = String.Empty;
    }
}