using OrderMs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure.Database.Configuration
{
        public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
        {
                public void Configure(EntityTypeBuilder<Incident> builder)
                {

                        builder.ToTable("Incident");

                        //* Este objeto representara una tabla en la BD
                        //TODO: Revisar si hace falta alguna validacion en la bd que sea necesaria
                        builder.HasKey(s => s.Id);
                        builder.Property(s => s.Id)
                                .HasConversion(IncidentId => IncidentId.Value, value => IncidentId.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.DestinyLocation)
                                .HasConversion(IncidentDestinyLocation => IncidentDestinyLocation.Value, value => IncidentDestinyLocation.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Date)
                                .HasConversion(IncidentDate => IncidentDate.Value, value => IncidentDate.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Description)
                                .HasConversion(IncidentDescription => IncidentDescription.Value, value => IncidentDescription.Create(value)!)
                                .IsRequired();

                }
        }
}
