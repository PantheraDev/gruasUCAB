using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Infrastructure.Database.Configuration
{
    public class ProviderDepartamentConfiguration : IEntityTypeConfiguration<ProviderDepartament>
    {
        public void Configure(EntityTypeBuilder<ProviderDepartament> builder)
        {
            
            builder.ToTable("ProviderDepartaments");
            builder.HasKey(pd => new { pd.ProviderId, pd.DepartamentId });

            builder.Property(pd => pd.DepartamentId)
            .HasConversion(departamentId => departamentId.Value, value => DepartamentId.Create(value)!)
            .IsRequired();

            builder.Property(pd => pd.ProviderId)
            .HasConversion(providerId => providerId.Value, value => ProviderId.Create(value)!)
            .IsRequired();

            builder.HasOne(pd => pd.Provider)
                   .WithMany(p => p.ProviderDepartaments)
                   .HasForeignKey(pd => pd.ProviderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pd => pd.Departament)
                   .WithMany(d => d.ProviderDepartaments)
                   .HasForeignKey(pd => pd.DepartamentId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}