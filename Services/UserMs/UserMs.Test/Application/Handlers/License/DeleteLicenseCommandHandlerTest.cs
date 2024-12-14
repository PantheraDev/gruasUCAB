using UserMs.Application.Handlers.License.Commands;
using UserMs.Core.Repositories;
using UserMs.Test.Data.MockData.License;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Application.Handlers.License
{
    public class DeleteLicenseCommandHandlerTest
    {
        private readonly DeleteLicenseCommandHandler _handler;
        private readonly Mock<ILicenseRepository> _licenseRepositoryMock;

        public DeleteLicenseCommandHandlerTest()
        {
            _licenseRepositoryMock = new Mock<ILicenseRepository>();

            _handler = new DeleteLicenseCommandHandler(_licenseRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldDeleteLicenseSuccessfully()
        {
            // Arrange
            var licenseId = LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingLicense = BuildDataContextFaker.BuildCreateLicenseEntity();
            var licensed = BuildDataContextFaker.GenerateLicensed();

            _licenseRepositoryMock.Setup(r => r.GetLicenseById(licenseId)).ReturnsAsync(existingLicense);
            _licenseRepositoryMock.Setup(r => r.DeleteLicenseAsync(licenseId)).Returns(licensed);

            var command = BuildDataContextFaker.BuildDeleteLicenseCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(licenseId, result);
            _licenseRepositoryMock.Verify(r => r.GetLicenseById(licenseId), Times.Once);
            _licenseRepositoryMock.Verify(r => r.DeleteLicenseAsync(licenseId), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenLicenseNotFound()
        {
            // Arrange
            var licenseId = LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));

            _licenseRepositoryMock.Setup(r => r.GetLicenseById(licenseId)).ReturnsAsync((Licensed)null);

            var command = BuildDataContextFaker.BuildDeleteLicenseCommand();

            // Act and Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}