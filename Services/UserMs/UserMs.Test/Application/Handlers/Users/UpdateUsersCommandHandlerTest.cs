using UserMs.Application.Handlers.User.Commands;
using UserMs.Core.Repositories;
using UserMs.Test.Data.MockData.User;
using Moq;
using Xunit;
using UserMs.Domain.Entities;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Test.Application.Handlers.User
{
    public class UpdateUsersCommandHandlerTest
    {
        private readonly UpdateUsersCommandHandler _handler;
        private readonly Mock<IUsersRepository> _usersRepositoryMock;

        public UpdateUsersCommandHandlerTest()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();

            _handler = new UpdateUsersCommandHandler(_usersRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldUpdateUsersSuccessfully()
        {
            // Arrange
            var usersId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingUsers = BuildDataContextFaker.BuildCreateUsersEntity();
            existingUsers.SetUserDelete(UserDelete.Create(false));
            var updatedUsers = BuildDataContextFaker.BuildUpdateUsersEntity();

            _usersRepositoryMock.Setup(r => r.GetUsersById(usersId)).ReturnsAsync(existingUsers);
            _usersRepositoryMock.Setup(r => r.UpdateUsersAsync(usersId, It.IsAny<Users>())).ReturnsAsync(existingUsers);

            var command = BuildDataContextFaker.BuildUpdateUsersCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _usersRepositoryMock.Verify(r => r.GetUsersById(usersId), Times.Once);
            _usersRepositoryMock.Verify(r => r.UpdateUsersAsync(usersId, It.IsAny<Users>()), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenUsersNotFound()
        {
            // Arrange
            var usersId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));

            _usersRepositoryMock.Setup(r => r.GetUsersById(usersId)).ReturnsAsync((Users)null);

            var command = BuildDataContextFaker.BuildUpdateUsersCommand();

            // Act and Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenUsersIsDeleted()
        {
            // Arrange
            var usersId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingUsers = BuildDataContextFaker.BuildCreateUsersEntity();
            existingUsers.SetUserDelete(UserDelete.Create(true));

            _usersRepositoryMock.Setup(r => r.GetUsersById(usersId)).ReturnsAsync(existingUsers);

            var command = BuildDataContextFaker.BuildUpdateUsersCommand();

            // Act and Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}