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
                public void Configure(EntityTypeBuilder<Provider> builder)
                {


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
                        // builder.Property(s => s.DepartmentId)
                        //         .HasConversion(DepartmentId => DepartmentId.Value, value => DepartmentId.Create(value)!)
                        //         .IsRequired();
                        builder.HasMany(s => s.Departments)
                                .WithMany(s => s.Providers)
                                .UsingEntity<ProviderDepartment>(
                                        j => j
                                                .HasOne(pd => pd.Department)
                                                .WithMany()
                                                .HasForeignKey(pd => pd.DepartmentId),
                                        j => j
                                                .HasOne(pd => pd.Provider)
                                                .WithMany()
                                                .HasForeignKey(pd => pd.ProviderId),
                                        j =>
                                        {
                                                j.HasKey(pd => pd.Id);
                                                j.Property(s => s.Id)
                                                .HasConversion(providerDepartmentId => providerDepartmentId.Value, value => ProviderDepartmentId.Create(value)!);
                                                j.ToTable("ProviderDepartments"); // Nombre de la tabla
                                        });




                        // debo probar esto 
                        /*builder.HasMany(p => p.ProviderDepartments)
                            .WithOne(pd => pd.Provider)
                            .HasForeignKey(pd=> pd.ProviderId);

                        builder.HasMany(p => p.ProviderDepartments)
                                .WithOne(pd => pd.Department) 
                                .HasForeignKey(pd => pd.DepartmentId);*/

                }
        }
}