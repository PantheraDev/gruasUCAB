using UserMs.Application.Commands.Drivers;
using UserMs.Application.Commands.User;
using UserMs.Application.Commands.License;
using UserMs.Application.Queries.Drivers;
using UserMs.Application.Queries.User;
using UserMs.Application.Queries.License;
using UserMs.Application.Dtos.Drivers.Request;
using UserMs.Application.Dtos.Users.Request;
using UserMs.Application.Dtos.License.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserMs.Domain.Entities;

namespace UserMs.Controllers
{
    [ApiController]
    [Route("user/license")]
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

    [ApiController]
    [Route("user/driver")]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;
        private readonly IMediator _mediator;

        public DriverController(ILogger<DriverController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriver(CreateDriverDto createDriverDto)
        {
            try
            {
                var command = new CreateDriverCommand(createDriverDto);
                var driverId = await _mediator.Send(command);
                return Ok(driverId);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an Driver {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Driver");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDriver()
        {
            try
            {
                var query = new GetDriverQuery();
                var drivers = await _mediator.Send(query);
                return Ok(drivers);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while getting Driver {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting Driver");
            }
        }

        [HttpGet("{driverId}")]
        public async Task<IActionResult> GetDriverById([FromRoute] Guid driverId)
        {
            try
            {
                UserId userId = UserId.Create(driverId);
                var query = new GetDriverByIdQuery(userId);
                var driver = await _mediator.Send(query);
                return Ok(driver);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while getting one Driver {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting one Driver");
            }
        }

        [HttpPut("{driverId}")]
        public async Task<IActionResult> UpdateDriver([FromRoute] Guid driverId, [FromBody] UpdateDriverDto driverDto)
        {
            try
            {
                UserId userId = UserId.Create(driverId);
                var command = new UpdateDriverCommand(userId, driverDto);
                var driver = await _mediator.Send(command);
                return Ok(driver);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an Driver {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Driver");
            }
        }

        [HttpDelete("{driverId}")]
        public async Task<IActionResult> DeleteDriver(Guid driverId)
        {
            try
            {
                UserId userId = UserId.Create(driverId);
                var command = new DeleteDriverCommand(userId);
                var driver = await _mediator.Send(command);
                return Ok(driver);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an Driver {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Driver");
            }
        }
    }

    [ApiController]
    [Route("user/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsers(CreateUsersDto createUsersDto)
        {
            try
            {
                var command = new CreateUsersCommand(createUsersDto);
                var userId = await _mediator.Send(command);
                return Ok(userId);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an User {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an User");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var query = new GetUsersQuery();
                var users = await _mediator.Send(query);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while getting User {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting User");
            }
        }

        [HttpGet("{usersId}")]
        public async Task<IActionResult> GetUsersById([FromRoute] Guid usersId)
        {
            try
            {
                UserId userId = UserId.Create(usersId);
                var query = new GetUsersByIdQuery(userId);
                var users = await _mediator.Send(query);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while getting one User {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting one User");
            }
        }

        [HttpPut("{usersId}")]
        public async Task<IActionResult> UpdateUsers([FromRoute] Guid usersId, [FromBody] UpdateUsersDto usersDto)
        {
            try
            {
                UserId userId = UserId.Create(usersId);
                var command = new UpdateUsersCommand(userId, usersDto);
                var users = await _mediator.Send(command);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an User {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an User");
            }
        }

        [HttpDelete("{usersId}")]
        public async Task<IActionResult> DeleteUsers(Guid usersId)
        {
            try
            {
                UserId userId = UserId.Create(usersId);
                var command = new DeleteUsersCommand(userId);
                var users = await _mediator.Send(command);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an User {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an User");
            }
        }
    }
}