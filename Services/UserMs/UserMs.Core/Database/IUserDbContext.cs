using UserMs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserMs.Core.Database
{
    public interface IUserDbContext
    {
        DbContext DbContext { get; }

        DbSet<Licensed> License { get; set; }

        DbSet<Users> Users { get; set; }

        DbSet<Driver> Drivers { get; set; }

        IDbContextTransactionProxy BeginTransaction();

        void ChangeEntityState<TEntity>(TEntity entity, EntityState state);

        Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default);
    }
}