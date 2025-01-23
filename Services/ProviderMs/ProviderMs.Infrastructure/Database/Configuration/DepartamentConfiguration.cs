using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProviderMs.Infrastructure.Database.Configuration
{
        public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
        {
                public void Configure(EntityTypeBuilder<Department> builder)
                {


                        builder.ToTable("Department");

                        builder.HasKey(s => s.Id);
                        builder.Property(s => s.Id)
                                .HasConversion(departmentId => departmentId.Value, value => DepartmentId.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Name)
                                .HasConversion(departmentName => departmentName.Value, value => DepartmentName.Create(value)!)
                                .IsRequired();
                        //     builder.Property(s => s.ProviderId)
                        //                         .HasConversion(ProviderId => ProviderId.Value, value => ProviderId.Create(value)!)
                        //                         .IsRequired();

                        builder.HasMany(s => s.Providers)
                                .WithMany(s => s.Departments)
                                .UsingEntity<ProviderDepartment>(
                                    j => j
                                            .HasOne(pd => pd.Provider)
                                            .WithMany()
                                            .HasForeignKey(pd => pd.ProviderId),
                                    j => j
                                            .HasOne(pd => pd.Department)
                                            .WithMany()
                                            .HasForeignKey(pd => pd.DepartmentId),

                                    j =>
                                    {
                                            j.HasKey(pd => pd.Id);
                                            j.Property(s => s.Id)
                                .HasConversion(providerDepartmentId => providerDepartmentId.Value, value => ProviderDepartmentId.Create(value)!);
                                            j.ToTable("ProviderDepartments"); // Nombre de la tabla
                                    });

                }
        }
}