using UserMs.Core.Database;
using UserMs.Infrastructure.Repositories;
using UserMs.Test.Data.MockData.License;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Infrastructure.Repositories
{
    public class LicenseRepositoryTest
    {
        private readonly LicenseRepository _repository;
        private readonly Mock<IUserDbContext> _contextMock;

        public LicenseRepositoryTest()
        {
            _contextMock = new Mock<IUserDbContext>();

            _repository = new LicenseRepository(_contextMock.Object);
        }

        [Fact]
        public async Task GetLicenseAsync_ShouldReturnListOfLicenses_WhenLicensesExist()
        {
            // Arrange
            var expectedLicenses = BuildDataContextFaker.BuildCreateLicenseEntityList();
            _contextMock.Setup(db => db.License);

            // Act
            var result = await _repository.GetLicenseAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLicenses, result);
        }

        [Fact]
        public async Task GetLicenseById_ShouldReturnLicense_WhenLicenseExists()
        {
            // Arrange
            var expectedLicense = BuildDataContextFaker.BuildCreateLicenseEntity();
            _contextMock.Setup(db => db.License.FindAsync(It.IsAny<LicenseId>()))
                        .ReturnsAsync(expectedLicense);

            // Act
            var result = await _repository.GetLicenseById(expectedLicense.LicenseId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLicense, result);
        }
        
        [Fact]
        public async Task GetLicenseById_ShouldReturnNull_WhenLicenseDoesNotExist()
        {
            // Arrange
            var nonExistentLicenseId = LicenseId.Create(new Guid("98765432-1a2b-34cd-56ef-789012345678")); // Different ID
            _contextMock.Setup(db => db.License.FindAsync(nonExistentLicenseId))
                        .ReturnsAsync((Licensed)null);

            // Act
            var result = await _repository.GetLicenseById(nonExistentLicenseId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddLicenseToDbContext()
        {
            // Arrange
            var license = BuildDataContextFaker.BuildCreateLicenseEntity();
            _contextMock.Setup(db => db.License.AddAsync(license, It.IsAny<CancellationToken>()));
            _contextMock.Setup(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()));

            // Act
            await _repository.AddAsync(license);

            // Assert
            _contextMock.Verify(db => db.License.AddAsync(license, It.IsAny<CancellationToken>()), Times.Once);
            _contextMock.Verify(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateLicenseAsync_ShouldUpdateExistingLicense()
        {
            // Arrange
            var licenseId = LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingLicense = BuildDataContextFaker.BuildCreateLicenseEntity();
            var updatedLicense = BuildDataContextFaker.BuildUpdateLicenseEntity();

            _contextMock.Setup(db => db.License.FindAsync(licenseId)).ReturnsAsync(existingLicense);

            // Act
            var result = await _repository.UpdateLicenseAsync(licenseId, updatedLicense);

            // Assert
            Assert.Equal(updatedLicense.LicenseId, result?.LicenseId);
            _contextMock.Verify(db => db.License.FindAsync(licenseId), Times.Once);
            _contextMock.Verify(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteLicenseAsync_ShouldDeleteExistingLicense()
        {
            // Arrange
            var licenseId = LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingLicense = BuildDataContextFaker.BuildCreateLicenseEntity();

            _contextMock.Setup(db => db.License.FindAsync(licenseId))
                .ReturnsAsync(existingLicense);

            // Act
            var result = await _repository.DeleteLicenseAsync(licenseId);

            // Assert
            Assert.Equal(existingLicense, result);
            _contextMock.Verify(db => db.License.FindAsync(licenseId), Times.Once);
            _contextMock.Verify(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}