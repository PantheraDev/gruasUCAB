using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProviderMs.Domain.Entities;

namespace ProviderMs.Core.Database
{
    public interface IApplicationDbContext
    {
        DbContext DbContext { get; }
        DbSet<Provider> Providers { get; set; }
        DbSet<Tow> Tows { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<ProviderDepartment> ProviderDepartments { get; set; }

        IDbContextTransactionProxy BeginTransaction();

        void ChangeEntityState<TEntity>(TEntity entity, EntityState state);

        Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default);
    }
}