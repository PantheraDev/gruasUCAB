using UserMs.Application.Handlers.Drives.Commands;
using UserMs.Core.Repositories;
using UserMs.Test.Data.MockData.Drivers;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Application.Handlers.Drivers
{
    public class CreateDriverCommandHandlerTest
    {
        private readonly CreateDriverCommandHandler _handler;
        private readonly Mock<IDriverRepository> _driverRepositoryMock;
        private readonly Mock<ILicenseRepository> _licenseRepositoryMock;

        public CreateDriverCommandHandlerTest()
        {
            _licenseRepositoryMock = new Mock<ILicenseRepository>();

            _driverRepositoryMock = new Mock<IDriverRepository>();

            _handler = new CreateDriverCommandHandler(_driverRepositoryMock.Object,_licenseRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldCreateDriverSuccessfully()
        {
            // Arrange
            var command = BuildDataContextFaker.BuildCreateDriverCommand(); // Populate with valid data
            var driverLicense = BuildDataContextFaker.BuildCreateLicenseEntity(); // Create a valid DriverLicense object
            var expectedUserId = BuildDataContextFaker.GetUserId();

            _licenseRepositoryMock.Setup(r => r.GetLicenseById(It.IsAny<LicenseId>())).ReturnsAsync(driverLicense);
            _driverRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Driver>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _licenseRepositoryMock.Verify(r => r.GetLicenseById(command.Driver.DriverLicenseId), Times.Once);
            _driverRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Driver>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCreateDriverThrowException()
        {
            var command = BuildDataContextFaker.BuildCreateDriverCommand();

            _driverRepositoryMock.Setup(x =>
                    x.AddAsync(It.IsAny<Driver>()))
                .ThrowsAsync(new Exception("Error"));

            var ex = await Assert.ThrowsAsync<Exception>(async () =>
            await _handler.Handle(command, new CancellationToken()));
            Assert.IsType<Exception>(ex);
        }
    }
}