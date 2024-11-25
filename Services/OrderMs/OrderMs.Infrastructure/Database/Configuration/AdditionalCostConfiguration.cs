using OrderMs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderMs.Domain.ValueObjects;
using OrderMs.Domain.Entities.ValueObjects;

namespace OrderMs.Infrastructure.Database.Configuration
{
        public class AdditionalCostConfiguration : IEntityTypeConfiguration<AdditionalCost>
        {
                public void Configure(EntityTypeBuilder<AdditionalCost> builder)
                {

                        builder.ToTable("AdditionalCost");

                        //* Este objeto representara una tabla en la BD
                        //TODO: Revisar si hace falta alguna validacion en la bd que sea necesaria
                        builder.HasKey(s => s.Id);
                        builder.Property(s => s.Id)
                                .HasConversion(AdditionalCostId => AdditionalCostId.Value, value => AdditionalCostId.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Value)
                                .HasConversion(AdditionalCostValue => AdditionalCostValue.Value, value => AdditionalCostValue.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Description)
                                .HasConversion(AdditionalCostDescription => AdditionalCostDescription.Value, value => AdditionalCostDescription.Create(value)!)
                                .IsRequired();

                }
        }
}
