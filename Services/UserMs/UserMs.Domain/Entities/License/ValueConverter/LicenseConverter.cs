using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserMs.Domain.Entities;

public class LicenseIdValueConverter : ValueConverter<LicenseId, Guid>
{
    public LicenseIdValueConverter() : base(
        v => v.Value,
        v => LicenseId.Create(v)
    ) { }
}

public class LicenseDateExpirationValueConverter : ValueConverter<LicenseDateExpiration, string>
{
    public LicenseDateExpirationValueConverter() : base(
        v => v.Value.ToString("yyyy-MM-dd"),
        v => LicenseDateExpiration.Create(DateTime.Parse(v))
    ) { }
}

public class LicenseNumberValueConverter : ValueConverter<LicenseNumber, string>
{
    public LicenseNumberValueConverter() : base(
        v => v.Value,
        v => LicenseNumber.Create(v)
    ) { }
}

public class LicenseDeleteConverter : ValueConverter<LicenseDelete, bool>
{
    public LicenseDeleteConverter() : base(
        u => u.Value,
        b => LicenseDelete.Create(b)
    ) { }
}