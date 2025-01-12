using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProviderMs.Application.Command;
using ProviderMs.Application.Queries;
using ProviderMs.Common.dto.Request;
using ProviderMs.Common.Exceptions;
using ProviderMs.ApplicationQueries;
using Microsoft.AspNetCore.Authorization;
using ProviderMs.Infrastructure.Exceptions;

namespace ProviderMs.Controllers
{
    [ApiController]
    [Route("provider")]

    public class ProviderDepartmentController : ControllerBase
    {
        private readonly ILogger<ProviderDepartmentController> _logger;
        private readonly IMediator _mediator;

        public ProviderDepartmentController(ILogger<ProviderDepartmentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpPost]
        [Route("addDepartment")]
        public async Task<IActionResult> CreateProviderDepartment([FromBody] CreateProviderDepartmentdto createProviderDepartmentDto)
        {
            try
            {
                var command = new CreateProviderDepartmentCommand(createProviderDepartmentDto);
                var providerDepartmentId = await _mediator.Send(command);
                return Ok(providerDepartmentId);
            }
            catch (ProviderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (ValidatorException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartamen: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an providerDepartment");
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpGet("getOneDepartment/{id}")]
        public async Task<IActionResult> GetProviderDepartment([FromRoute] Guid id)
        {
            try
            {
                var command = new GetProviderDepartmentQuery(id);
                var providerDepartment = await _mediator.Send(command);
                return Ok(providerDepartment);
            }
            catch (ProviderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an providerDepartment: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an providerDepartment");
            }
        }

         [Authorize(Policy = "AdminProviderOnly")]
        [HttpGet("getAllDepartments")]
        public async Task<IActionResult> GetAllProviderDepartments()
        {
            try
            {
                var command = new GetAllProviderDepartmentsQuery();
                var providerDepartment = await _mediator.Send(command);
                return Ok(providerDepartment);
            }
            catch (ProviderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an providerDepartment: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an providerDepartment");
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpGet("getDepartmentsByProvider/{providerId}")]
        public async Task<IActionResult> GetDepartmentsByProvider([FromRoute] Guid providerId)
        {
            try
            {
                var command = new GetDepartmentsByProviderQuery(providerId);
                var providerDepartment = await _mediator.Send(command);
                return Ok(providerDepartment);
            }
            catch (ProviderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an providerDepartment: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an providerDepartment");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        [Route("updateDepartment/{id}")]
        public async Task<IActionResult> UpdateProviderDepartment([FromRoute] Guid id, [FromBody] UpdateProviderDepartmentDto updateProviderDepartmentDto)
        {
            try
            {
                var command = new UpdateProviderDepartmentCommand(id, updateProviderDepartmentDto);
                var providerDepartmentId = await _mediator.Send(command);
                return Ok(providerDepartmentId);
            }
            catch (ProviderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an providerDepartment: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an providerDepartment");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [Route("deleteDepartment/{id}")]
        public async Task<IActionResult> DeleteProviderDepartment([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteProviderDepartmentCommand(id);
                var providerDepartmentId = await _mediator.Send(command);
                return Ok(providerDepartmentId);
            }
            catch (ProviderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an providerDepartment: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an providerDepartment: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an providerDepartment");
            }
        }
    }
}