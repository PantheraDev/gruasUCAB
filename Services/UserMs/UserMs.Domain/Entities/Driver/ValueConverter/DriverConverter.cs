/*using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserMs.Domain.Entities;
public class DriverAvailableValueConverter : ValueConverter<DriverAvailable, bool>
{
    public DriverAvailableValueConverter() : base(
        v => v.IsAvailable, // Convierte DriverAvailable a bool
        v => new DriverAvailable(v) // Convierte bool a DriverAvailable
    ) { }
}*/