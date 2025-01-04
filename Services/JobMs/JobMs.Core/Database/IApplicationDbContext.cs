using Microsoft.EntityFrameworkCore;
using JobMs.Domain.Notification;

namespace JobMs.Core.Database
{
    public interface IApplicationDbContext
    {
        DbContext DbContext { get; }

        DbSet<NotificationEntity> Notification {get; set;}
       
        IDbContextTransactionProxy BeginTransaction();

        void ChangeEntityState<TEntity>(TEntity entity, EntityState state);

        Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default);
    }
}