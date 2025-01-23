using OrderMs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderMs.Domain.ValueObjects;
using OrderMs.Domain.Entities.ValueObjects;

namespace OrderMs.Infrastructure.Database.Configuration
{
        public class OrderConfiguration : IEntityTypeConfiguration<Order>
        {
                public void Configure(EntityTypeBuilder<Order> builder)
                {

                        builder.ToTable("Order");

                        //* Este objeto representara una tabla en la BD
                        //TODO: Revisar si hace falta alguna validacion en la bd que sea necesaria
                        builder.HasKey(s => s.Id);
                        builder.Property(s => s.Id)
                                .HasConversion(OrderId => OrderId.Value, value => OrderId.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.DestinyLocation)
                                .HasConversion(OrderDestinyLocation => OrderDestinyLocation.Value, value => OrderDestinyLocation.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.OriginLocation)
                                .HasConversion(OrderOriginLocation => OrderOriginLocation!.Value, value => OrderOriginLocation.Create(value)!)
                                .IsRequired(false);
                        builder.Property(s => s.TotalCost)
                                .HasConversion(OrderTotalCost => OrderTotalCost.Value, value => OrderTotalCost.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Date)
                                .HasConversion(OrderDate => OrderDate.Value, value => OrderDate.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.State)
                                .HasConversion<string>()
                                .IsRequired();
                        builder.Property(s => s.IncidentId)
                                .HasConversion(IncidentId => IncidentId.Value, value => IncidentId.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.PolicyId)
                                .HasConversion(PolicyId => PolicyId.Value, value => PolicyId.Create(value)!)
                                .IsRequired();
                        // builder.Property(s => s.AdditionalCostId)
                        //         .HasConversion(AdditionalCostId => AdditionalCostId!.Value, value => AdditionalCostId.Create(value)!);
                        builder.Property(s => s.TowId)
                                .HasConversion(TowId => TowId!.Value, value => TowId.Create(value)!);
                        // builder.HasOne(s => s.AdditionalCost)
                        //         .WithMany()
                        //         .HasForeignKey(s => s.Id)
                        //         .IsRequired(false);


                }
        }
}
