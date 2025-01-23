using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Infrastructure.Database.Configuration
{
    public class ProviderDepartmentConfiguration : IEntityTypeConfiguration<ProviderDepartment>
    {
        public void Configure(EntityTypeBuilder<ProviderDepartment> builder)
        {
            builder.ToTable("ProviderDepartments");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                    .HasConversion(providerDepartmentId => providerDepartmentId.Value, value => ProviderDepartmentId.Create(value)!)
                    .IsRequired();

            // builder.Property(pd => pd.ProviderId)
            //         .HasConversion(providerId => providerId.Value, value => ProviderId.Create(value)!)
            //         .IsRequired();

            // builder.Property(pd => pd.DepartmentId)
            //         .HasConversion(departmentId => departmentId.Value, value => DepartmentId.Create(value)!)
            //         .IsRequired();
        }
    }
}