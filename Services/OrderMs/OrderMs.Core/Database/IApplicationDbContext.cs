using Microsoft.EntityFrameworkCore;
using OrderMs.Domain.Entities;

namespace OrderMs.Core.Database
{
    public interface IApplicationDbContext
    {
        DbContext DbContext { get; }
        DbSet<Client> Clients { get; set; }
        DbSet<Fee> Fees { get; set; }
        DbSet<AdditionalCost> AdditionalCosts { get; set; }
        DbSet<Incident> Incidents { get; set; }
        DbSet<InsuredVehicle> InsuredVehicles { get; set; }
        DbSet<Policy> Policies { get; set; }
        DbSet<Order> Orders { get; set; }

        IDbContextTransactionProxy BeginTransaction();

        void ChangeEntityState<TEntity>(TEntity entity, EntityState state);

        Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default);
    }
}