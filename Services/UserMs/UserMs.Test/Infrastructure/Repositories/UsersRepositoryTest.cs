using UserMs.Core.Database;
using UserMs.Infrastructure.Repositories;
using UserMs.Test.Data.MockData.User;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Infrastructure.Repositories
{
    public class UsersRepositoryTest
    {
        private readonly UsersRepository _repository;
        private readonly Mock<IUserDbContext> _contextMock;

        public UsersRepositoryTest()
        {
            _contextMock = new Mock<IUserDbContext>();

            _repository = new UsersRepository(_contextMock.Object);
        }
/*
        [Fact]
        public async Task GetLicenseAsync_ShouldReturnListOfLicenses_WhenLicensesExist()
        {
            // Arrange
            var expectedLicenses = BuildDataContextFaker.BuildCreateLicenseEntityList();
           _contextMock.Setup(db => db.License.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectedLicenses);

            // Act
            var result = await _repository.GetLicenseAsync();

            // Assert
            Assert.Equal(expectedLicenses, result);
        }
*/
        [Fact]
        public async Task GetUsersById_ShouldReturnUsers_WhenUsersExists()
        {
            // Arrange
            var expectedUsers = BuildDataContextFaker.BuildCreateUsersEntity();
            _contextMock.Setup(db => db.Users.FindAsync(It.IsAny<UserId>()))
                        .ReturnsAsync(expectedUsers);

            // Act
            var result = await _repository.GetUsersById(expectedUsers.UserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsers, result);
        }
        
        [Fact]
        public async Task GetUsersById_ShouldReturnNull_WhenUsersDoesNotExist()
        {
            // Arrange
            var nonExistentUsersId = UserId.Create(new Guid("98765432-1a2b-34cd-56ef-789012345678")); // Different ID
            _contextMock.Setup(db => db.Users.FindAsync(nonExistentUsersId))
                        .ReturnsAsync((Users)null);

            // Act
            var result = await _repository.GetUsersById(nonExistentUsersId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddUsersToDbContext()
        {
            // Arrange
            var users = BuildDataContextFaker.BuildCreateUsersEntity();
            _contextMock.Setup(db => db.Users.AddAsync(users, It.IsAny<CancellationToken>()));
            _contextMock.Setup(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()));

            // Act
            await _repository.AddAsync(users);

            // Assert
            _contextMock.Verify(db => db.Users.AddAsync(users, It.IsAny<CancellationToken>()), Times.Once);
            _contextMock.Verify(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUsersAsync_ShouldUpdateExistingUsers()
        {
            // Arrange
            var usersId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingUsers = BuildDataContextFaker.BuildCreateUsersEntity();
            var updatedUsers = BuildDataContextFaker.BuildUpdateUsersEntity();

            _contextMock.Setup(db => db.Users.FindAsync(usersId)).ReturnsAsync(existingUsers);

            // Act
            var result = await _repository.UpdateUsersAsync(usersId, updatedUsers);

            // Assert
            Assert.Equal(updatedUsers.UserId, result?.UserId);
            _contextMock.Verify(db => db.Users.FindAsync(usersId), Times.Once);
            _contextMock.Verify(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteUsersAsync_ShouldDeleteExistingUsers()
        {
            // Arrange
            var usersId = UserId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var existingUsers = BuildDataContextFaker.BuildCreateUsersEntity();

            _contextMock.Setup(db => db.Users.FindAsync(usersId))
                .ReturnsAsync(existingUsers);

            // Act
            var result = await _repository.DeleteUsersAsync(usersId);

            // Assert
            Assert.Equal(existingUsers, result);
            _contextMock.Verify(db => db.Users.FindAsync(usersId), Times.Once);
            _contextMock.Verify(db => db.SaveEfContextChanges(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}