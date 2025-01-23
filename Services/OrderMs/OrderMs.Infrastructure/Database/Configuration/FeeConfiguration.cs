using OrderMs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderMs.Domain.ValueObjects;
using OrderMs.Domain.Entities.ValueObjects;

namespace OrderMs.Infrastructure.Database.Configuration
{
        public class FeeConfiguration : IEntityTypeConfiguration<Fee>
        {
                public void Configure(EntityTypeBuilder<Fee> builder)
                {

                        builder.ToTable("Fee");

                        //* Este objeto representara una tabla en la BD
                        //TODO: Revisar si hace falta alguna validacion en la bd que sea necesaria
                        builder.HasKey(s => s.Id);
                        builder.Property(s => s.Id)
                                .HasConversion(FeeId => FeeId.Value, value => FeeId.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.BasePrice)
                                .HasConversion(FeeBasePrice => FeeBasePrice.Value, value => FeeBasePrice.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Radius)
                                .HasConversion(FeeRadius => FeeRadius.Value, value => FeeRadius.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.PriceXKm)
                                .HasConversion(FeePriceXKm => FeePriceXKm.Value, value => FeePriceXKm.Create(value)!)
                                .IsRequired();

                }
        }
}
