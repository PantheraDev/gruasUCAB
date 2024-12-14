using OrderMs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure.Database.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {

            builder.ToTable("Client");

            //* Este objeto representara una tabla en la BD
            //TODO: Revisar si hace falta alguna validacion en la bd que sea necesaria
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                    .HasConversion(clientId => clientId.Value, value => ClientId.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Name)
                    .HasConversion(clientName => clientName.Value, value => ClientName.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.LastName)
                    .HasConversion(clientLastName => clientLastName.Value, value => ClientLastName.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Ci)
                    .HasConversion(clientCi => clientCi.Value, value => ClientCi.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Phone)
                    .HasConversion(clientPhone => clientPhone.Value, value => ClientPhone.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.Address)
                    .HasConversion(clientAddress => clientAddress.Value, value => ClientAddress.Create(value)!)
                    .IsRequired();
            builder.Property(s => s.BirthDate)
                    .HasConversion(clientBirthDate => clientBirthDate.Value, value => ClientBirthDate.Create(value)!)
                    .IsRequired();

        }
    }
}
