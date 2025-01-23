using UserMs.Application.Dtos.Users.Response;
using MediatR;

namespace UserMs.Application.Queries.User
{
    public class GetUsersQuery : IRequest<List<GetUsersDto>>
    {
        public Guid userId { get; set; }
        public string? userEmail { get; set; }
        public string? userPassword { get; set; }
        public string? userType { get; set; }
        public Guid? userProvider { get; set; }
        public Guid? userDepartament { get; set; }
    }
}