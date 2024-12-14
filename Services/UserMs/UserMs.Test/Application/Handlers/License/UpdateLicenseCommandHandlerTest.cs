using UserMs.Application.Handlers.License.Commands;
using UserMs.Core.Repositories;
using UserMs.Test.Data.MockData.License;
using Moq;
using Xunit;
using UserMs.Domain.Entities;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Test.Application.Handlers.License
{
    public class UpdateLicenseCommandHandlerTest
    {
        private readonly UpdateLicenseCommandHandler _handler;
        private readonly Mock<ILicenseRepository> _licenseRepositoryMock;

        public UpdateLicenseCommandHandlerTest()
        {
            _licenseRepositoryMock = new Mock<ILicenseRepository>();

            _handler = new UpdateLicenseCommandHandler(_licenseRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldUpdateLicenseSuccessfully()
        {
            // Arrange
            var licenseId = LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingLicense = BuildDataContextFaker.BuildCreateLicenseEntity();
            existingLicense.SetLicenseDelete(LicenseDelete.Create(false));
            var updatedLicense = BuildDataContextFaker.BuildUpdateLicenseEntity();

            _licenseRepositoryMock.Setup(r => r.GetLicenseById(licenseId)).ReturnsAsync(existingLicense);
            _licenseRepositoryMock.Setup(r => r.UpdateLicenseAsync(licenseId, It.IsAny<Licensed>())).ReturnsAsync(existingLicense);

            var command = BuildDataContextFaker.BuildUpdateLicenseCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _licenseRepositoryMock.Verify(r => r.GetLicenseById(licenseId), Times.Once);
            _licenseRepositoryMock.Verify(r => r.UpdateLicenseAsync(licenseId, It.IsAny<Licensed>()), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenLicenseNotFound()
        {
            // Arrange
            var licenseId = LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));

            _licenseRepositoryMock.Setup(r => r.GetLicenseById(licenseId)).ReturnsAsync((Licensed)null);

            var command = BuildDataContextFaker.BuildUpdateLicenseCommand();

            // Act and Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenLicenseIsDeleted()
        {
            // Arrange
            var licenseId = LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingLicense = BuildDataContextFaker.BuildCreateLicenseEntity();
            existingLicense.SetLicenseDelete(LicenseDelete.Create(true)); // Mark the license as deleted

            _licenseRepositoryMock.Setup(r => r.GetLicenseById(licenseId)).ReturnsAsync(existingLicense);

            var command = BuildDataContextFaker.BuildUpdateLicenseCommand();

            // Act and Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}