using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.PortableExecutable;

namespace ProviderMs.Infrastructure.Database.Configuration
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder){


                builder.ToTable("Provider");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                    .HasConversion(providerId => providerId.Value, value => ProviderId.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Name)
                    .HasConversion(providerName => providerName.Value, value => ProviderName.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Phone)
                    .HasConversion(providerPhone => providerPhone.Value, value => ProviderPhone.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Email)
                    .HasConversion(providerEmail => providerEmail.Value, value => ProviderEmail.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.RIF)
                    .HasConversion(providerRif => providerRif.Value, value => ProviderRIF.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Address)
                    .HasConversion(providerAddress => providerAddress.Value, value => ProviderAddress.Create(value)!)
                    .IsRequired();



                // debo probar esto 
                /*builder.HasMany(p => p.ProviderDepartaments)
                    .WithOne(pd => pd.Provider)
                    .HasForeignKey(pd=> pd.ProviderId);

                builder.HasMany(p => p.ProviderDepartaments)
                        .WithOne(pd => pd.Departament) 
                        .HasForeignKey(pd => pd.DepartamentId);*/

        }
    }
}