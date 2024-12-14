using UserMs.Application.Handlers.User.Commands;
using UserMs.Core.Repositories;
using UserMs.Test.Data.MockData.User;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Application.Handlers.User
{
    public class DeleteUsersCommandHandlerTest
    {
        private readonly DeleteUsersCommandHandler _handler;
        private readonly Mock<IUsersRepository> _usersRepositoryMock;

        public DeleteUsersCommandHandlerTest()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();

            _handler = new DeleteUsersCommandHandler(_usersRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldDeleteUsersSuccessfully()
        {
            // Arrange
            var usersId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingUsers = BuildDataContextFaker.BuildCreateUsersEntity();
            var users = BuildDataContextFaker.GenerateUsers();

            _usersRepositoryMock.Setup(r => r.GetUsersById(usersId)).ReturnsAsync(existingUsers);
            _usersRepositoryMock.Setup(r => r.DeleteUsersAsync(usersId)).Returns(users);

            var command = BuildDataContextFaker.BuildDeleteUsersCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(usersId, result);
            _usersRepositoryMock.Verify(r => r.GetUsersById(usersId), Times.Once);
            _usersRepositoryMock.Verify(r => r.DeleteUsersAsync(usersId), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenUsersNotFound()
        {
            // Arrange
            var usersId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));

            _usersRepositoryMock.Setup(r => r.GetUsersById(usersId)).ReturnsAsync((Users)null);

            var command = BuildDataContextFaker.BuildDeleteUsersCommand();

            // Act and Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}