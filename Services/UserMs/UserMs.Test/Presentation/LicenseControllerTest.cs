using UserMs.Application.Commands.License;
using UserMs.Application.Queries.License;
using UserMs.Controllers;
using UserMs.Test.Data.MockData.License;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using UserMs.Domain.Entities;

namespace UserMs.Test.Presentation
{
    public  class LicenseControllerTest
    {
        private readonly LicenseController _licenseController;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<LicenseController>> _loggerMock;

        public LicenseControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<LicenseController>>();
            _licenseController = new LicenseController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task CreateLicense_ShouldReturnOk33()
        {
            var createLicenseDto = BuildDataContextFaker.GenerateCreateLicenseDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateLicenseCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(LicenseId.Create(Guid.NewGuid()));
            var result = await _licenseController.CreateLicense(createLicenseDto);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateLicense_ShouldReturnError()
        {
            var createLicenseDto = BuildDataContextFaker.GenerateCreateLicenseDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateLicenseCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _licenseController.CreateLicense(createLicenseDto);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task GetLicense_ShouldReturnOk33()
        {
            var getLicenseDto = BuildDataContextFaker.GenerateGetLicenseDtoList();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetLicenseQuery>(), It.IsAny<CancellationToken>()))
                .Returns(getLicenseDto);
            var result = await _licenseController.GetLicense();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetLicense_ShouldReturnError()
        {
            var getLicenseDto = BuildDataContextFaker.GenerateGetLicenseDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetLicenseQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _licenseController.GetLicense();
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task GetLicenseById_ShouldReturnOk33()
        {
            var licenseId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var getLicenseDto = BuildDataContextFaker.GenerateGetLicenseDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetLicenseByIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns(getLicenseDto);
            var result = await _licenseController.GetLicenseById(licenseId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetLicenseById_ShouldReturnError()
        {
            var licenseId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var getLicenseDto = BuildDataContextFaker.GenerateGetLicenseDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetLicenseByIdQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _licenseController.GetLicenseById(licenseId);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task UpdateLicense_ShouldReturnOk33()
        {
            var licenseId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var updateLicenseDto = BuildDataContextFaker.GenerateUpdateLicenseDto();
            var license = BuildDataContextFaker.GenerateLicensed();
            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateLicenseCommand>(), It.IsAny<CancellationToken>()))
                .Returns(license);
            var result = await _licenseController.UpdateLicense(licenseId,updateLicenseDto);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateLicense_ShouldReturnError()
        {
            var licenseId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var updateLicenseDto = BuildDataContextFaker.GenerateUpdateLicenseDto();
            var license = BuildDataContextFaker.GenerateLicensed();
            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateLicenseCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _licenseController.UpdateLicense(licenseId,updateLicenseDto);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }

        [Fact]
        public async Task DeleteLicense_ShouldReturnOk33()
        {
            var licenseId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var id = BuildDataContextFaker.GetLicenseId();
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteLicenseCommand>(), It.IsAny<CancellationToken>()))
                .Returns(id);
            var result = await _licenseController.DeleteLicense(licenseId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteLicense_ShouldReturnError()
        {
            var licenseId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822");
            var id = BuildDataContextFaker.GetLicenseId();
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteLicenseCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("test error"));
            try
            {
                await _licenseController.DeleteLicense(licenseId);
            }
            catch (Exception ex)
            {
                Assert.IsType<Exception>(ex);
                Assert.Equal("test error", ex.Message);
            }
        }
    }
}