using UserMs.Core.Database;
using UserMs.Infrastructure.Repositories;
using UserMs.Test.Data.MockData.Drivers;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Infrastructure.Repositories
{
    public class DriverRepositoryTest
    {
        private readonly DriverRepository _repository;
        private readonly Mock<IUserDbContext> _contextMock;

        public DriverRepositoryTest()
        {
            _contextMock = new Mock<IUserDbContext>();

            _repository = new DriverRepository(_contextMock.Object);
        }
/*
        [Fact]
        public async Task GetLicenseAsync_ShouldReturnListOfLicenses_WhenLicensesExist()
        {
            // Arrange
            var expectedLicenses = BuildDataContextFaker.BuildCreateLicenseEntityList();
           _contextMock.Setup(db => db.License.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectedLicenses);

            // Act
            var result = await _repository.GetLicenseAsync();

            // Assert
            Assert.Equal(expectedLicenses, result);
        }
*/
        [Fact]
        public async Task GetDriverById_ShouldReturnDriver_WhenDriverExists()
        {
            // Arrange
            var expectedDriver = BuildDataContextFaker.BuildCreateDriverEntity();
            _contextMock.Setup(db => db.Drivers.FindAsync(It.IsAny<UserId>()))
                        .ReturnsAsync(expectedDriver);

            // Act
            var result = await _repository.GetDriverById(expectedDriver.UserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDriver, result);
        }
        
        [Fact]
        public async Task GetDriverById_ShouldReturnNull_WhenDriverDoesNotExist()
        {
            // Arrange
            var nonExistentDriverId = UserId.Create(new Guid("98765432-1a2b-34cd-56ef-789012345678")); // Different ID
            _contextMock.Setup(db => db.Drivers.FindAsync(nonExistentDriverId))
                        .ReturnsAsync((Driver)null);

            // Act
            var result = await _repository.GetDriverById(nonExistentDriverId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddDriverToDbContext()
        {
            // Arrange
            var driver = BuildDataContextFaker.BuildCreateDriverEntity();
            _contextMock.Setup(db => db.Drivers.AddAsync(driver, It.IsAny<CancellationToken>()));
            _contextMock.Setup(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()));

            // Act
            await _repository.AddAsync(driver);

            // Assert
            _contextMock.Verify(db => db.Drivers.AddAsync(driver, It.IsAny<CancellationToken>()), Times.Once);
            _contextMock.Verify(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDriverAsync_ShouldUpdateExistingDriver()
        {
            // Arrange
            var driverId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingDriver = BuildDataContextFaker.BuildCreateDriverEntity();
            var updatedDriver = BuildDataContextFaker.BuildUpdateDriverEntity();

            _contextMock.Setup(db => db.Drivers.FindAsync(driverId)).ReturnsAsync(existingDriver);

            // Act
            var result = await _repository.UpdateDriverAsync(driverId, updatedDriver);

            // Assert
            Assert.Equal(updatedDriver.UserId, result?.UserId);
            _contextMock.Verify(db => db.Drivers.FindAsync(driverId), Times.Once);
            _contextMock.Verify(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDriverAsync_ShouldDeleteExistingDriver()
        {
            // Arrange
            var driverId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingDriver = BuildDataContextFaker.BuildCreateDriverEntity();

            _contextMock.Setup(db => db.Drivers.FindAsync(driverId))
                .ReturnsAsync(existingDriver);

            // Act
            var result = await _repository.DeleteDriverAsync(driverId);

            // Assert
            Assert.Equal(existingDriver, result);
            _contextMock.Verify(db => db.Drivers.FindAsync(driverId), Times.Once);
            _contextMock.Verify(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}