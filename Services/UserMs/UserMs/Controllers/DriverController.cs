using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMs.Application.Commands.Drivers;
using UserMs.Application.Dtos.Drivers.Request;
using UserMs.Application.Queries.Drivers;
using UserMs.Domain.Entities;

namespace UserMs.Controllers
{
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

        [Authorize(Policy = "AdminProviderOnly")]
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

        [Authorize(Policy = "AdminProviderOperatorOnly")]
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

        [Authorize(Policy = "AdminProviderOperatorOnly")]
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

        [Authorize(Policy = "AdminProviderOnly")]
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

        [Authorize(Policy = "AdminProviderOnly")]
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

}