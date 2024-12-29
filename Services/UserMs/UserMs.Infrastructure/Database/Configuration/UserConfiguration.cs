using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserMs.Domain.Entities;

namespace UserMs.Infrastructure.Database.Configuration
{
    public class LicenseConfiguration : IEntityTypeConfiguration<Licensed>
    {
        public void Configure(EntityTypeBuilder<Licensed> builder)
        {
            builder.HasKey(s => s.LicenseId);
            builder.Property(s => s.LicenseDateExpiration).IsRequired();
            builder.Property(s => s.LicenseNumber).IsRequired();
            builder.Property(s => s.LicenseDelete).HasDefaultValueSql("false");
        }
    }

    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(s => s.UserId);
            builder.Property(s => s.UserEmail).IsRequired();
            builder.Property(s => s.UserPassword).IsRequired();
            builder.Property(s => s.UserDelete).HasDefaultValueSql("false");
            builder.Property(s => s.UsersType).IsRequired().HasMaxLength(50).HasConversion<string>();
            builder.Property(s => s.UserProvider).IsRequired();
            builder.Property(s => s.UserDepartament).IsRequired();
        }
    }

    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(s => s.UserId);
            builder.Property(s => s.UserEmail).IsRequired();
            builder.Property(s => s.UserPassword).IsRequired();
            builder.Property(s => s.UserDelete).HasDefaultValueSql("false");
            builder.Property(s => s.DriverAvailable).IsRequired().HasMaxLength(50).HasConversion<string>();
            builder.Property(s => s.DriverLicenseId).IsRequired();
            builder.Property(s => s.UserProvider).IsRequired();
            builder.Property(s => s.UserDepartament).IsRequired();
        }
    }
}