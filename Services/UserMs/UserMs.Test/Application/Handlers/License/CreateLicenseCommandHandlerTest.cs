using UserMs.Application.Handlers.License.Commands;
using UserMs.Core.Repositories;
using UserMs.Test.Data.MockData.License;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Application.Handlers.License
{
    public class CreateLicenseCommandHandlerTest
    {
        private readonly CreateLicenseCommandHandler _handler;
        private readonly Mock<ILicenseRepository> _licenseRepositoryMock;

        public CreateLicenseCommandHandlerTest()
        {
            _licenseRepositoryMock = new Mock<ILicenseRepository>();

            _handler = new CreateLicenseCommandHandler(_licenseRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldCreateLicenseSuccess()
        {
            var command = BuildDataContextFaker.BuildCreateLicenseCommand();

            _licenseRepositoryMock.Setup(x => 
                    x.AddAsync(It.IsAny<Licensed>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, new CancellationToken());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldCreateLicenseThrowException()
        {
            var command = BuildDataContextFaker.BuildCreateLicenseCommand();

            _licenseRepositoryMock.Setup(x =>
                    x.AddAsync(It.IsAny<Licensed>()))
                .ThrowsAsync(new Exception("Error"));

            var ex = await Assert.ThrowsAsync<Exception>(async () =>
            await _handler.Handle(command, new CancellationToken()));
            Assert.IsType<Exception>(ex);
        }
    }
}