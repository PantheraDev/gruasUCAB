using UserMs.Application.Handlers.Drives.Commands;
using UserMs.Core.Repositories;
using UserMs.Test.Data.MockData.Drivers;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Application.Handlers.Drivers
{
    public class DeleteDriverCommandHandlerTest
    {
        private readonly DeleteDriverCommandHandler _handler;
        private readonly Mock<IDriverRepository> _driverRepositoryMock;

        public DeleteDriverCommandHandlerTest()
        {
            _driverRepositoryMock = new Mock<IDriverRepository>();

            _handler = new DeleteDriverCommandHandler(_driverRepositoryMock.Object);
        }

                [Fact]
        public async Task ShouldDeleteDriverSuccessfully()
        {
            // Arrange
            var driverId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingDriver = BuildDataContextFaker.BuildCreateDriverEntity();
            var driver = BuildDataContextFaker.GenerateDriver();

            _driverRepositoryMock.Setup(r => r.GetDriverById(driverId)).ReturnsAsync(existingDriver);
            _driverRepositoryMock.Setup(r => r.DeleteDriverAsync(driverId)).Returns(driver);

            var command = BuildDataContextFaker.BuildDeleteDriverCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(driverId, result);
            _driverRepositoryMock.Verify(r => r.GetDriverById(driverId), Times.Once);
            _driverRepositoryMock.Verify(r => r.DeleteDriverAsync(driverId), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenDriverNotFound()
        {
            // Arrange
            var driverId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));

            _driverRepositoryMock.Setup(r => r.GetDriverById(driverId)).ReturnsAsync((Driver)null);

            var command = BuildDataContextFaker.BuildDeleteDriverCommand();

            // Act and Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}