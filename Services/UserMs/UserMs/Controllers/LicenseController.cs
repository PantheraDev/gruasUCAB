using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMs.Application.Commands.License;
using UserMs.Application.Dtos.License.Request;
using UserMs.Application.Queries.License;
using UserMs.Domain.Entities;

namespace UserMs.Controllers
{

    [ApiController]
    [Route("user/license")]
    [Authorize(Policy = "AdminProviderOnly")]
    public class LicenseController : ControllerBase
    {
        private readonly ILogger<LicenseController> _logger;
        private readonly IMediator _mediator;

        public LicenseController(ILogger<LicenseController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLicense(CreateLicenseDto createLicenseDto)
        {
            try
            {
                var command = new CreateLicenseCommand(createLicenseDto);
                var licenseId = await _mediator.Send(command);
                return Ok(licenseId);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an License {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an License");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLicense()
        {
            try
            {
                var query = new GetLicenseQuery();
                var license = await _mediator.Send(query);
                return Ok(license);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while getting License {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting License");
            }
        }

        [HttpGet("{licenseId}")]
        public async Task<IActionResult> GetLicenseById([FromRoute] Guid licenseId)
        {
            try
            {
                LicenseId licensedId = LicenseId.Create(licenseId);
                var query = new GetLicenseByIdQuery(licensedId);
                var license = await _mediator.Send(query);
                return Ok(license);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while getting one License {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting one License");
            }
        }

        [HttpPut("{licenseId}")]
        public async Task<IActionResult> UpdateLicense([FromRoute] Guid licenseId, [FromBody] UpdateLicenseDto licenseDto)
        {
            try
            {
                LicenseId licensedId = LicenseId.Create(licenseId);
                var command = new UpdateLicenseCommand(licensedId, licenseDto);
                var license = await _mediator.Send(command);
                return Ok(license);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an License {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an License");
            }
        }

        [HttpDelete("{licenseId}")]
        public async Task<IActionResult> DeleteLicense(Guid licenseId)
        {
            try
            {
                LicenseId licensedId = LicenseId.Create(licenseId);
                var command = new DeleteLicenseCommand(licensedId);
                var license = await _mediator.Send(command);
                return Ok(license);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an License {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an License");
            }
        }
    }
}
