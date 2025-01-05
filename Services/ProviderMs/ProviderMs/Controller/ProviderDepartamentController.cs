using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProviderMs.Application.Queries;
using ProviderMs.Common.Exceptions;
using ProviderMs.ApplicationQueries;
using ProviderMs.Application.Command;
using Microsoft.AspNetCore.Authorization;

namespace ProviderMs.Controllers
{
    [ApiController]
    [Route("/providerDepartament")]

    public class ProviderDepartamentController : ControllerBase
    {
        private readonly ILogger<ProviderDepartamentController> _logger;
        private readonly IMediator _mediator;

        public ProviderDepartamentController(ILogger<ProviderDepartamentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllProviderDepartaments()
        {
            try
            {
                var query = new GetAllProviderDepartamentsQuery();
                var providerDepartaments = await _mediator.Send(query);
                return Ok(providerDepartaments);
            }
            catch (ProviderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an provider: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an provider: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an provider: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search provider: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search providers");
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProviderDepartament([FromRoute] Guid id)
        {
            try
            {
                var command = new GetProviderDepartamentQuery(id);
                var providerDepartament = await _mediator.Send(command);
                return Ok(providerDepartament);
            }
            catch (ProviderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an provider: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an provider: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an provider: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an provider: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an provider");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [Route("{providerId}/departaments/{departamentId}")]
        public async Task<IActionResult> DeleteProviderDepartament([FromRoute] Guid providerId, [FromRoute] Guid departamentId)
        {
            try
            {
                var command = new DeleteProviderDepartamentCommand(providerId, departamentId);
                var providerDepartamentId = await _mediator.Send(command);
                return Ok(providerDepartamentId);
            }
            catch (ProviderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an provider: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an provider: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an provider: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an provider: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an provider");
            }
        }
    }
}