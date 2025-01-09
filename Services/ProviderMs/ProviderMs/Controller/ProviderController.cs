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
    [Route("/provider")]

    public class ProviderController : ControllerBase
    {
        private readonly ILogger<ProviderController> _logger;
        private readonly IMediator _mediator;

        public ProviderController(ILogger<ProviderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> CreateProvider([FromBody] CreateProviderdto createProviderDto)
        {
            try
            {
                var command = new CreateProviderCommand(createProviderDto);
                var providerId = await _mediator.Send(command);
                return Ok(providerId);
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
            catch (ValidatorException e)
            {
                _logger.LogError("An error occurred while trying to create an provider: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an provider: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an provider");
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllProviders()
        {
            try
            {
                var query = new GetAllProvidersQuery();
                var providers = await _mediator.Send(query);
                if (!providers.Any())
                {
                    return NoContent();
                }
                return Ok(providers);
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
        public async Task<IActionResult> GetProvider([FromRoute] Guid id)
        {
            try
            {
                var command = new GetProviderQuery(id);
                var provider = await _mediator.Send(command);
                return Ok(provider);
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

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProvider([FromRoute] Guid id, [FromBody] UpdateProviderDto updateProviderDto)
        {
            try
            {
                var command = new UpdateProviderCommand(id, updateProviderDto);
                var providerId = await _mediator.Send(command);
                return Ok(providerId);
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
                _logger.LogError("An error occurred while trying to update an provider: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an provider");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProvider([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteProviderCommand(id);
                var providerId = await _mediator.Send(command);
                return Ok(providerId);
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