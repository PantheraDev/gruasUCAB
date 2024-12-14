using UserMs.Application.Commands.Drivers;
using UserMs.Application.Dtos.Drivers.Request;
using UserMs.Application.Dtos.Drivers.Response;
using UserMs.Domain.Entities;

namespace UserMs.Test.Data.MockData.Drivers
{
    public static class BuildDataContextFaker
    {
        public static CreateDriverCommand BuildCreateDriverCommand()
        {
            var driver = new CreateDriverDto()
            {
                UserId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail = UserEmail.Create("test@gmail.com"),
                UserPassword = UserPassword.Create("contraseña"),
                DriverAvailable = DriverAvailable.Create(false),
                DriverLicenseId = LicenseId.Create(new Guid("69d4827b-0cf5-4deb-9097-9671a21aa9b9"))
            };
            return new CreateDriverCommand(driver);
        }

        public static DeleteDriverCommand BuildDeleteDriverCommand()
        {
            var driverId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            return new DeleteDriverCommand(driverId);
        }

        public static UpdateDriverCommand BuildUpdateDriverCommand()
        {
            var driverId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var driver = new UpdateDriverDto()
            {
                UserEmail = UserEmail.Create("test@gmail.com"),
                UserPassword = UserPassword.Create("contraseña"),
                DriverAvailable = DriverAvailable.Create(false),
                DriverLicenseId = LicenseId.Create(new Guid("69d4827b-0cf5-4deb-9097-9671a21aa9b9"))
            };
            return new UpdateDriverCommand(driverId,driver);
        }

        public static Driver BuildCreateDriverEntity()
        {
            var driver = new Driver(
                UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail.Create("test@gmail.com"),
                UserPassword.Create("contraseña"),
                DriverAvailable.Create(false)
                );
                
            return driver;
        }

        public static Licensed BuildCreateLicenseEntity()
        {
            var license = new Licensed(
                LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")), 
                LicenseDateExpiration.Create(new DateTime(2026-09-10)), 
                LicenseNumber.Create("109031444")
                );
                
            return license;
        }

        public static List<Driver> BuildCreateDriverEntityList()
        {
            var driver1 = new Driver(
                UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail.Create("test@gmail.com"),
                UserPassword.Create("contraseña"),
                DriverAvailable.Create(false)
            );

            var driver2 = new Driver(
                UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209820")),
                UserEmail.Create("prueba@gmail.com"),
                UserPassword.Create("password"),
                DriverAvailable.Create(true)
            );

            return new List<Driver> { driver1, driver2 };
        }

        public static Driver BuildUpdateDriverEntity()
        {
            var driver = new Driver(
                UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail.Create("juanPablo@gmail.com"),
                UserPassword.Create("665531"),
                DriverAvailable.Create(true)
            );

            return driver;
        }
        
        public static CreateDriverDto GenerateCreateDriverDto()
        {
            return new CreateDriverDto()
            {
                UserId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail = UserEmail.Create("test@gmail.com"),
                UserPassword = UserPassword.Create("contraseña"),
                DriverAvailable = DriverAvailable.Create(false),
                DriverLicenseId = LicenseId.Create(new Guid("69d4827b-0cf5-4deb-9097-9671a21aa9b9"))
            };
        }

        public static Task<List<GetDriverDto>> GenerateGetDriverDtoList()
        {
            var driverDto = new GetDriverDto()
            {
                UserId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"),
                UserEmail = "test@gmail.com",
                UserPassword = "contraseña",
                DriverAvailable = false,
                DriverLicenseId = new Guid("69d4827b-0cf5-4deb-9097-9671a21aa9b9")
            };

            return Task.FromResult(new List<GetDriverDto>() { driverDto });
        }

        public static Task<GetDriverDto> GenerateGetDriverDto()
        {
            var driverDto = new GetDriverDto()
            {
                UserId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"),
                UserEmail = "test@gmail.com",
                UserPassword = "contraseña",
                DriverAvailable = false,
                DriverLicenseId = new Guid("69d4827b-0cf5-4deb-9097-9671a21aa9b9")
            };

            return Task.FromResult(driverDto);
        }

        public static UpdateDriverDto GenerateUpdateDriverDto()
        {
            return new UpdateDriverDto()
            {
                UserEmail = UserEmail.Create("test@gmail.com"),
                UserPassword = UserPassword.Create("contraseña"),
                DriverAvailable = DriverAvailable.Create(false),
                DriverLicenseId = LicenseId.Create(new Guid("69d4827b-0cf5-4deb-9097-9671a21aa9b9"))
            };
        }

        public static Task<Driver> GenerateDriver()
        {
            var driver = new Driver(
                UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                UserEmail.Create("test@gmail.com"),
                UserPassword.Create("contraseña"),
                DriverAvailable.Create(false)
            );

            return Task.FromResult(driver);
        }

        public static Task<UserId> GetUserId()
        {
            var userId = UserId.Create(Guid.NewGuid());

            return Task.FromResult(userId);
        }
    }
}