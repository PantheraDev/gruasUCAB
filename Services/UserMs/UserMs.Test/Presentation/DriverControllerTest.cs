using UserMs.Application.Commands.Drivers;
using UserMs.Application.Queries.Drivers;
using UserMs.Controllers;
using UserMs.Test.Data.MockData.Drivers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Presentation
{
    public  class DriverControllerTest
    {
        private readonly DriverController _driverController;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<DriverController>> _loggerMock;

        public DriverControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<DriverController>>();
            _driverController = new DriverController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task CreateDriver_ShouldReturnOk33()
        {
            var createDriverDto = BuildDataContextFaker.GenerateCreateDriverDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateDriverCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(UserId.Create(Guid.NewGuid()));
            var result = await _driverController.CreateDriver(createDriverDto);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateDriver_ShouldReturnError()
        {
            var createDriverDto = BuildDataContextFaker.GenerateCreateDriverDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateDriverCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _driverController.CreateDriver(createDriverDto);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task GetDriver_ShouldReturnOk33()
        {
            var getDriverDto = BuildDataContextFaker.GenerateGetDriverDtoList();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetDriverQuery>(), It.IsAny<CancellationToken>()))
                .Returns(getDriverDto);
            var result = await _driverController.GetDriver();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetDriver_ShouldReturnError()
        {
            var getDriverDto = BuildDataContextFaker.GenerateGetDriverDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetDriverQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _driverController.GetDriver();
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task GetDriverById_ShouldReturnOk33()
        {
            var userId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var getDriverDto = BuildDataContextFaker.GenerateGetDriverDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetDriverByIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns(getDriverDto);
            var result = await _driverController.GetDriverById(userId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetDriverById_ShouldReturnError()
        {
            var userId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var getDriverDto = BuildDataContextFaker.GenerateGetDriverDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetDriverByIdQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _driverController.GetDriverById(userId);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task UpdateDriver_ShouldReturnOk33()
        {
            var userId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var updateDriverDto = BuildDataContextFaker.GenerateUpdateDriverDto();
            var driver = BuildDataContextFaker.GenerateDriver();
            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateDriverCommand>(), It.IsAny<CancellationToken>()))
                .Returns(driver);
            var result = await _driverController.UpdateDriver(userId,updateDriverDto);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateDriver_ShouldReturnError()
        {
            var userId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var updateDriverDto = BuildDataContextFaker.GenerateUpdateDriverDto();
            var driver = BuildDataContextFaker.GenerateDriver();
            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateDriverCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _driverController.UpdateDriver(userId,updateDriverDto);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task DeleteDriver_ShouldReturnOk33()
        {
            var userId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var id = BuildDataContextFaker.GetUserId();
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteDriverCommand>(), It.IsAny<CancellationToken>()))
                .Returns(id);
            var result = await _driverController.DeleteDriver(userId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteDriver_ShouldReturnError()
        {
            var userId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var id = BuildDataContextFaker.GetUserId();
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteDriverCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _driverController.DeleteDriver(userId);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }
    }
}