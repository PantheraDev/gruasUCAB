using UserMs.Application.Commands.User;
using UserMs.Application.Queries.User;
using UserMs.Controllers;
using UserMs.Test.Data.MockData.User;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Presentation
{
    public  class UsersControllerTest
    {
        private readonly UsersController _usersController;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<UsersController>> _loggerMock;

        public UsersControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<UsersController>>();
            _usersController = new UsersController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task CreateUsers_ShouldReturnOk33()
        {
            var createUsersDto = BuildDataContextFaker.GenerateCreateUsersDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateUsersCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(UserId.Create(Guid.NewGuid()));
            var result = await _usersController.CreateUsers(createUsersDto);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateUsers_ShouldReturnError()
        {
            var createUsersDto = BuildDataContextFaker.GenerateCreateUsersDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateUsersCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _usersController.CreateUsers(createUsersDto);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task GetUsers_ShouldReturnOk33()
        {
            var getUsersDto = BuildDataContextFaker.GenerateGetUsersDtoList();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .Returns(getUsersDto);
            var result = await _usersController.GetUsers();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnError()
        {
            var getUsersDto = BuildDataContextFaker.GenerateGetUsersDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _usersController.GetUsers();
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task GetUsersById_ShouldReturnOk33()
        {
            var usersId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var getUsersDto = BuildDataContextFaker.GenerateGetUsersDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetUsersByIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns(getUsersDto);
            var result = await _usersController.GetUsersById(usersId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUsersById_ShouldReturnError()
        {
            var usersId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var getUsersDto = BuildDataContextFaker.GenerateGetUsersDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetUsersByIdQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _usersController.GetUsersById(usersId);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task UpdateUsers_ShouldReturnOk33()
        {
            var usersId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var updateUsersDto = BuildDataContextFaker.GenerateUpdateUsersDto();
            var users = BuildDataContextFaker.GenerateUsers();
            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateUsersCommand>(), It.IsAny<CancellationToken>()))
                .Returns(users);
            var result = await _usersController.UpdateUsers(usersId,updateUsersDto);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateUsers_ShouldReturnError()
        {
            var usersId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var updateUsersDto = BuildDataContextFaker.GenerateUpdateUsersDto();
            var users = BuildDataContextFaker.GenerateUsers();
            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateUsersCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _usersController.UpdateUsers(usersId,updateUsersDto);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task DeleteUsers_ShouldReturnOk33()
        {
            var usersId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var id = BuildDataContextFaker.GetUserId();
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteUsersCommand>(), It.IsAny<CancellationToken>()))
                .Returns(id);
            var result = await _usersController.DeleteUsers(usersId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteUsers_ShouldReturnError()
        {
            var usersId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var id = BuildDataContextFaker.GetUserId();
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteUsersCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _usersController.DeleteUsers(usersId);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }
    }
}