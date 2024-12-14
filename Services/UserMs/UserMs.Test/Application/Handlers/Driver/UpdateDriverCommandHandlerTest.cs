using UserMs.Application.Handlers.Drives.Commands;
using UserMs.Core.Repositories;
using UserMs.Test.Data.MockData.Drivers;
using Moq;
using Xunit;
using UserMs.Domain.Entities;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Test.Application.Handlers.Drivers
{
    public class UpdateDriverCommandHandlerTest
    {
        private readonly UpdateDriverCommandHandler _handler;
        private readonly Mock<IDriverRepository> _driverRepositoryMock;

        public UpdateDriverCommandHandlerTest()
        {
            _driverRepositoryMock = new Mock<IDriverRepository>();

            _handler = new UpdateDriverCommandHandler(_driverRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldUpdateDriverSuccessfully()
        {
            // Arrange
            var driverId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingDriver = BuildDataContextFaker.BuildCreateDriverEntity();
            existingDriver.SetUserDelete(UserDelete.Create(false));
            var updatedDriver = BuildDataContextFaker.BuildUpdateDriverEntity();

            _driverRepositoryMock.Setup(r => r.GetDriverById(driverId)).ReturnsAsync(existingDriver);
            _driverRepositoryMock.Setup(r => r.UpdateDriverAsync(driverId, It.IsAny<Driver>())).ReturnsAsync(existingDriver);

            var command = BuildDataContextFaker.BuildUpdateDriverCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _driverRepositoryMock.Verify(r => r.GetDriverById(driverId), Times.Once);
            _driverRepositoryMock.Verify(r => r.UpdateDriverAsync(driverId, It.IsAny<Driver>()), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenDriverNotFound()
        {
            // Arrange
            var driverId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));

            _driverRepositoryMock.Setup(r => r.GetDriverById(driverId)).ReturnsAsync((Driver)null);

            var command = BuildDataContextFaker.BuildUpdateDriverCommand();

            // Act and Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenDriverIsDeleted()
        {
            // Arrange
            var driverId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingDriver = BuildDataContextFaker.BuildCreateDriverEntity();
            existingDriver.SetUserDelete(UserDelete.Create(true));

            _driverRepositoryMock.Setup(r => r.GetDriverById(driverId)).ReturnsAsync(existingDriver);

            var command = BuildDataContextFaker.BuildUpdateDriverCommand();

            // Act and Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}