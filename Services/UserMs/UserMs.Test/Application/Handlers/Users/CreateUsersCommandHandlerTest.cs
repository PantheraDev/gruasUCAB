using UserMs.Application.Handlers.User.Commands;
using UserMs.Core.Repositories;
using UserMs.Test.Data.MockData.User;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Application.Handlers.User
{
    public class CreateUsersCommandHandlerTest
    {
        private readonly CreateUsersCommandHandler _handler;
        private readonly Mock<IUsersRepository> _usersRepositoryMock;

        public CreateUsersCommandHandlerTest()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();

            _handler = new CreateUsersCommandHandler(_usersRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldCreateUsersSuccess()
        {
            var command = BuildDataContextFaker.BuildCreateUsersCommand();

            _usersRepositoryMock.Setup(x => 
                    x.AddAsync(It.IsAny<Users>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, new CancellationToken());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldCreateUsersThrowException()
        {
            var command = BuildDataContextFaker.BuildCreateUsersCommand();

            _usersRepositoryMock.Setup(x =>
                    x.AddAsync(It.IsAny<Users>()))
                .ThrowsAsync(new Exception("Error"));

            var ex = await Assert.ThrowsAsync<Exception>(async () =>
            await _handler.Handle(command, new CancellationToken()));
            Assert.IsType<Exception>(ex);
        }
    }
}