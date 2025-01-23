using JobMs.Domain.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobMs.Infrastructure.Database.Configuration
{
        public class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
        {
                public void Configure(EntityTypeBuilder<NotificationEntity> builder)
                {

                        builder.ToTable("Notification");

                        //* Este objeto representara una tabla en la BD
                        //TODO: Revisar si hace falta alguna validacion en la bd que sea necesaria
                        builder.HasKey(s => s.Id);
                        builder.Property(s => s.Id)
                                .IsRequired();
                        builder.Property(s => s.DeviceToken)
                                .IsRequired();
                }
        }
}