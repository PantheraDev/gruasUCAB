using OrderMs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderMs.Domain.ValueObjects;
using OrderMs.Domain.Entities.ValueObjects;

namespace OrderMs.Infrastructure.Database.Configuration
{
        public class InsuredVehicleConfiguration : IEntityTypeConfiguration<InsuredVehicle>
        {
                public void Configure(EntityTypeBuilder<InsuredVehicle> builder)
                {

                        builder.ToTable("InsuredVehicle");

                        //* Este objeto representara una tabla en la BD
                        //TODO: Revisar si hace falta alguna validacion en la bd que sea necesaria
                        builder.HasKey(s => s.Id);
                        builder.Property(s => s.Id)
                                .HasConversion(InsuredVehicleId => InsuredVehicleId.Value, value => InsuredVehicleId.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Weight)
                                .HasConversion(InsuredVehicleWeight => InsuredVehicleWeight.Value, value => InsuredVehicleWeight.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.LicensePlate)
                                .HasConversion(InsuredVehicleLicensePlate => InsuredVehicleLicensePlate.Value, value => InsuredVehicleLicensePlate.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Brand)
                                .HasConversion(InsuredVehicleBrand => InsuredVehicleBrand.Value, value => InsuredVehicleBrand.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Model)
                                .HasConversion(InsuredVehicleModel => InsuredVehicleModel.Value, value => InsuredVehicleModel.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Year)
                                .HasConversion(InsuredVehicleYear => InsuredVehicleYear.Value, value => InsuredVehicleYear.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Color)
                                .HasConversion(InsuredVehicleColor => InsuredVehicleColor.Value, value => InsuredVehicleColor.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.ClientId)
                                .HasConversion(ClientId => ClientId.Value, value => ClientId.Create(value)!)
                                .IsRequired();

                }
        }
}
