using UserMs.Domain.Entities;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using UserMs.Core.Database;

namespace UserMs.Infrastructure.Database
{
    public class UserDbContext : DbContext, IUserDbContext
    {
        public UserDbContext(
           DbContextOptions<UserDbContext> options
       )
           : base(options) { }

        public DbContext DbContext
        {
            get { return this; }
        }

        public virtual DbSet<Licensed> License { get; set; } = null!;

        public virtual DbSet<Users> Users { get; set; } = null!;

        public virtual DbSet<Driver> Drivers { get; set; } = null!;

        public IDbContextTransactionProxy BeginTransaction()
        {
            return new DbContextTransactionProxy(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Licensed>()   
                        .Property(u => u.LicenseId)   
                        .HasConversion(new LicenseIdValueConverter());
            modelBuilder.Entity<Licensed>()   
                        .Property(u => u.LicenseDateExpiration)   
                        .HasConversion(new LicenseDateExpirationValueConverter());
            modelBuilder.Entity<Licensed>()   
                        .Property(u => u.LicenseNumber)   
                        .HasConversion(new LicenseNumberValueConverter());
            modelBuilder.Entity<Licensed>()
                        .Property(d => d.LicenseDelete)
                        .HasConversion<LicenseDeleteConverter>();

            modelBuilder.Entity<Driver>()   
                        .Property(u => u.UserId)   
                        .HasConversion(new UserIdValueConverter());
            modelBuilder.Entity<Driver>()   
                        .Property(u => u.UserEmail)   
                        .HasConversion(new UserEmailValueConverter());
            modelBuilder.Entity<Driver>()   
                        .Property(u => u.UserPassword)
                        .HasConversion(new UserPasswordValueConverter());
            modelBuilder.Entity<Driver>()
                        .Property(d => d.UserDelete)
                        .HasConversion<UserDeleteConverter>();
            modelBuilder.Entity<Driver>()
                        .HasOne(d => d.DriverLicense)
                        .WithOne()
                        .HasForeignKey<Driver>(d => d.DriverLicenseId);
            modelBuilder.Entity<Driver>()   
                        .Property(d => d.DriverAvailable);
            modelBuilder.Entity<Driver>()
                        .Property(d => d.UserProvider)
                        .HasConversion(new UserProviderValueConverter());
            modelBuilder.Entity<Driver>()
                        .Property(d => d.UserDepartament)
                        .HasConversion(new UserDepartamentValueConverter());

            modelBuilder.Entity<Users>()   
                        .Property(u => u.UserId)   
                        .HasConversion(new UserIdValueConverter());
            modelBuilder.Entity<Users>()   
                        .Property(u => u.UserEmail)   
                        .HasConversion(new UserEmailValueConverter());
            modelBuilder.Entity<Users>()   
                        .Property(u => u.UserPassword)
                        .HasConversion(new UserPasswordValueConverter());
            modelBuilder.Entity<Users>()
                        .Property(d => d.UserDelete)
                        .HasConversion<UserDeleteConverter>();
            modelBuilder.Entity<Users>()   
                        .Property(u => u.UsersType);
            modelBuilder.Entity<Users>()
                        .Property(u => u.UserProvider)
                        .HasConversion(new UserProviderValueConverter());
            modelBuilder.Entity<Users>()
                        .Property(u => u.UserDepartament)
                        .HasConversion(new UserDepartamentValueConverter());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }

        public virtual void SetPropertyIsModifiedToFalse<TEntity, TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression
        )
            where TEntity : class
        {
            Entry(entity).Property(propertyExpression).IsModified = false;
        }

        public virtual void ChangeEntityState<TEntity>(TEntity entity, EntityState state)
        {
            if (entity != null)
            {
                Entry(entity).State = state;
            }
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default
        )
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is Base
                    && (e.State == EntityState.Added || e.State == EntityState.Modified)
                );

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((Base)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((Base)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    ((Base)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                    Entry((Base)entityEntry.Entity).Property(x => x.CreatedAt).IsModified =
                        false;
                    Entry((Base)entityEntry.Entity).Property(x => x.CreatedBy).IsModified =
                        false;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(
            string user,
            CancellationToken cancellationToken = default
        )
        {
            var state = new List<EntityState> { EntityState.Added, EntityState.Modified };

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Base && state.Any(s => e.State == s));

            var dt = DateTime.UtcNow;

            foreach (var entityEntry in entries)
            {
                var entity = (Base)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = dt;
                    entity.CreatedBy = user;
                    Entry(entity).Property(x => x.UpdatedAt).IsModified = false;
                    Entry(entity).Property(x => x.UpdatedBy).IsModified = false;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = dt;
                    entity.UpdatedBy = user;
                    Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveEfContextChanges(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(cancellationToken) >= 0;
        }

        public async Task<bool> SaveEfContextChanges(
            string user,
            CancellationToken cancellationToken = default
        )
        {
            return await SaveChangesAsync(user, cancellationToken) >= 0;
        }
    }
}