
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProviderMs.Application.Command;
using ProviderMs.Common.dto.Request;
using ProviderMs.Application.Queries;
using ProviderMs.ApplicationQueries;
using ProviderMs.Common.Exceptions;
using ProviderMs.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace ProviderMs.Controllers
{
    [ApiController]
    [Route("provider/department")]

    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMediator _mediator;

        public DepartmentController(ILogger<DepartmentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> CreatedDepartment([FromBody] CreateDepartmentdto createDepartmentdto)
        {
            try
            {
                var command = new CreateDepartmentCommand(createDepartmentdto);
                var DepartmentId = await _mediator.Send(command);
                return Ok(DepartmentId);
            }
            catch (DepartmentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (ValidatorException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Department");
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var query = new GetAllDepartmentsQuery();
                var Departments = await _mediator.Send(query);
                return Ok(Departments);
            }
            catch (DepartmentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search Department: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search Department");
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment([FromRoute] Guid id)
        {
            try
            {
                var command = new GetDepartmentQuery(id);
                var Department = await _mediator.Send(command);
                return Ok(Department);
            }
            catch (DepartmentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an Department: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an Department");
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
        {
            try
            {
                var command = new UpdateDepartmentCommand(id, updateDepartmentDto);
                var DepartmentId = await _mediator.Send(command);
                return Ok(DepartmentId);
            }
            catch (DepartmentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an Department: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Department");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteDepartmentCommand(id);
                var DepartmentId = await _mediator.Send(command);
                return Ok(DepartmentId);
            }
            catch (DepartmentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Department: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an Department: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Department");
            }
        }

    }
}