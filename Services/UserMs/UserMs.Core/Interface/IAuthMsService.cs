using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMs.Core.Database;
using UserMs.Domain.Entities;

namespace UserMs.Core.Interface
{
    public interface IAuthMsService
    {
        Task<string> CreateUserAsync(string userEmail, string userPassword);
        Task<string> DeleteUserAsync(UserId userId);
        Task AssignClientRoleToUser(
            Guid userId,
            string roleName
        );
        Task<Guid> GetUserByUserName(UserEmail userName);
        Task UpdateUser(UserId id, Base user);
    }
}