using Microsoft.EntityFrameworkCore;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProviderMs.Common.Enums;

namespace ProviderMs.Infrastructure.Database.Configuration
{
    public class TowConfiguration : IEntityTypeConfiguration<Tow>
    {
        public void Configure(EntityTypeBuilder<Tow> builder){

                builder.ToTable("Tows");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                    .HasConversion(vehicleId => vehicleId.Value, value => VehicleId.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Color)
                    .HasConversion(vehicleName => vehicleName.Value, value => VehicleColor.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Year)
                    .HasConversion(vehicleYear => vehicleYear.Value, value => VehicleYear.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Model)
                    .HasConversion(vehicleModel => vehicleModel.Value, value => VehicleModel.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Brand)
                    .HasConversion(vehiclebrand => vehiclebrand.Value, value => VehicleBrand.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.LicensePlate)
                    .HasConversion(vehicleLicensePlate => vehicleLicensePlate.Value, value => VehicleLicensePlate.Create(value)!)
                    .IsRequired();
                builder.Property(s => s.TowLocation)
                    .HasConversion(towlocation => towlocation.Value, value => TowLocation.Create(value)!)
                    .IsRequired();
                builder.Property(s => s.TowAvailability)
                    .HasConversion(TowAvailability => TowAvailability.ToString(), value =>  (TowAvailability)Enum.Parse(typeof(TowAvailability),value)!)
                    .IsRequired();
                builder.Property(s => s.TowType)
                    .HasConversion(towtype => towtype.ToString(), value =>  (TowType)Enum.Parse(typeof(TowType),value)!)
                    .IsRequired();
                builder.Property(v => v.ProviderId) // Configuración de la clave foránea
                    .HasConversion(providerId => providerId.Value, value => ProviderId.Create(value))
                    .IsRequired();
                builder.Property(s => s.TowDriver)
                    .HasConversion(towdriver=> towdriver.Value, value => TowDriver.Create(value)!)
                    .IsRequired();

                builder.HasOne(v => v.provider) // Configuración de la relación
                    .WithMany(p => p.Tows)
                    .HasForeignKey(v => v.ProviderId)
                    .OnDelete(DeleteBehavior.Cascade); // Opcional: Configura el comportamiento de borrado en cascada
        }
    }
}