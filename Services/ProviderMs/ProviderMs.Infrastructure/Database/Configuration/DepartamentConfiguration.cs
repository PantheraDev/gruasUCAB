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
    public class DepartamentConfiguration : IEntityTypeConfiguration<Departament>
    {
        public void Configure(EntityTypeBuilder<Departament> builder){


            builder.ToTable("Departament");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                    .HasConversion(departamentId => departamentId.Value, value => DepartamentId.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Name)
                    .HasConversion(departamentName => departamentName.Value, value => DepartamentName.Create(value)!)
                    .IsRequired();
        }
    }
}