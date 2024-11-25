using OrderMs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderMs.Domain.ValueObjects;
using OrderMs.Domain.Entities.ValueObjects;

namespace OrderMs.Infrastructure.Database.Configuration
{
        public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
        {
                public void Configure(EntityTypeBuilder<Policy> builder)
                {

                        builder.ToTable("Policy");

                        //* Este objeto representara una tabla en la BD
                        //TODO: Revisar si hace falta alguna validacion en la bd que sea necesaria
                        builder.HasKey(s => s.Id);
                        builder.Property(s => s.Id)
                                .HasConversion(PolicyId => PolicyId.Value, value => PolicyId.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.Coverage)
                                .HasConversion(PolicyCoverage => PolicyCoverage.Value, value => PolicyCoverage.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.IssueDate)
                                .HasConversion(PolicyIssueDate => PolicyIssueDate.Value, value => PolicyIssueDate.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.ExpirationDate)
                                .HasConversion(PolicyExpirationDate => PolicyExpirationDate.Value, value => PolicyExpirationDate.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.InsuredVehicleId)
                                .HasConversion(InsuredVehicleId => InsuredVehicleId.Value, value => InsuredVehicleId.Create(value)!)
                                .IsRequired();
                        builder.Property(s => s.FeeId)
                                .HasConversion(FeeId => FeeId.Value, value => FeeId.Create(value)!)
                                .IsRequired();


                }
        }
}
