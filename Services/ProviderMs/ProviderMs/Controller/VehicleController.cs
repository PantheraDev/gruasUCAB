using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProviderMs.Application.Command;
using ProviderMs.Common.dto.Request;
using ProviderMs.Application.Queries;
using ProviderMs.Common.Exceptions;
using ProviderMs.Infrastructure.Exceptions;
using ProviderMs.ApplicationQueries;
using Microsoft.AspNetCore.Authorization;

namespace ProviderMs.Controllers
{
    [ApiController]
    [Route("provider/tow")]

    public class TowController : ControllerBase
    {
        private readonly ILogger<TowController> _logger;
        private readonly IMediator _mediator;

        public TowController(ILogger<TowController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateTowdto createTowDto)
        {
            try
            {
                var command = new CreateTowCommand(createTowDto);
                var vehicleId = await _mediator.Send(command);
                return Ok(vehicleId);
            }
            catch (VehicleNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an tow: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an tow: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an tow: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (ValidatorException e)
            {
                _logger.LogError("An error occurred while trying to create an tow: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an tow: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an tow");
            }
        }

        [Authorize(Policy = "AdminProviderOperatorOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllTows()
        {
            try
            {
                var query = new GetAllTowsQuery();
                var vehicles = await _mediator.Send(query);
                return Ok(vehicles);
            }
            catch (VehicleNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an tow: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an tow: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an tow: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search tows: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search tows");
            }
        }

        [Authorize(Policy = "AdminProviderOperatorOnly")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle([FromRoute] Guid id)
        {
            try
            {
                var command = new GetTowQuery(id);
                var vehicle = await _mediator.Send(command);
                return Ok(vehicle);
            }
            catch (ProviderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an vehicle: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an vehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an vehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an vehicle: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an vehicle");
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateVehicle([FromRoute] Guid id, [FromBody] UpdateTowDto updateTowDto)
        {
            try
            {
                var command = new UpdateTowCommand(id, updateTowDto);
                var vehicleId = await _mediator.Send(command);
                return Ok(vehicleId);
            }
            catch (VehicleNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an vehicle: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an vehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an vehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an vehicle: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an vehicle");
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteTowCommand(id);
                var vehicleId = await _mediator.Send(command);
                return Ok(vehicleId);
            }
            catch (VehicleNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an vehicle: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an vehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an vehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an vehicle: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an vehicle");
            }
        }

    }
}