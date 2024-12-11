
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderMs.Application.Commands;
using OrderMs.Common.Dtos.Request;
using OrderMs.Application.Queries;
using OrderMs.ApplicationQueries;
using OrderMs.Common.Exceptions;
using OrderMs.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace OrderMs.Controllers
{
    [ApiController]
    [Route("/insured-vehicle")]
    public class InsuredVehicleController : ControllerBase
    {
        private readonly ILogger<InsuredVehicleController> _logger;
        private readonly IMediator _mediator;

        public InsuredVehicleController(ILogger<InsuredVehicleController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> CreateInsuredVehicle([FromBody] CreateInsuredVehicleDto createInsuredVehicleDto)
        {
            try
            {
                var command = new CreateInsuredVehicleCommand(createInsuredVehicleDto);
                var InsuredVehicleId = await _mediator.Send(command);
                return Ok(InsuredVehicleId);
            }
            catch (InsuredVehicleNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (ValidatorException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an InsuredVehicle");
            }
        }

        [Authorize(Policy = "AdminOperatorOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllInsuredVehicles()
        {
            try
            {
                var query = new GetAllInsuredVehiclesQuery();
                var InsuredVehicles = await _mediator.Send(query);
                return Ok(InsuredVehicles);
            }
            catch (InsuredVehicleNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search InsuredVehicles: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search InsuredVehicles");
            }
        }

        [Authorize(Policy = "AdminOperatorOnly")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInsuredVehicle([FromRoute] Guid id)
        {
            try
            {
                var command = new GetInsuredVehicleQuery(id);
                var InsuredVehicle = await _mediator.Send(command);
                return Ok(InsuredVehicle);
            }
            catch (InsuredVehicleNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an InsuredVehicle: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an InsuredVehicle");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateInsuredVehicle([FromRoute] Guid id, [FromBody] UpdateInsuredVehicleDto updateInsuredVehicleDto)
        {
            try
            {
                var command = new UpdateInsuredVehicleCommand(id, updateInsuredVehicleDto);
                var InsuredVehicleId = await _mediator.Send(command);
                return Ok(InsuredVehicleId);
            }
            catch (InsuredVehicleNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an InsuredVehicle: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an InsuredVehicle");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteInsuredVehicle([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteInsuredVehicleCommand(id);
                var InsuredVehicleId = await _mediator.Send(command);
                return Ok(InsuredVehicleId);
            }
            catch (InsuredVehicleNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an InsuredVehicle: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an InsuredVehicle: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an InsuredVehicle");
            }
        }

    }
}