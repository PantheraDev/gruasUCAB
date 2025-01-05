
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
    [Route("/additional-cost")]
    [Authorize(Policy = "AdminDriverOperatorOnly")]
    public class AdditionalCostController : ControllerBase
    {
        private readonly ILogger<AdditionalCostController> _logger;
        private readonly IMediator _mediator;

        public AdditionalCostController(ILogger<AdditionalCostController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdditionalCost([FromBody] CreateAdditionalCostDto createAdditionalCostDto)
        {
            try
            {
                var command = new CreateAdditionalCostCommand(createAdditionalCostDto);
                var AdditionalCostId = await _mediator.Send(command);
                return Ok(AdditionalCostId);
            }
            catch (AdditionalCostNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (ValidatorException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an AdditionalCost");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdditionalCosts()
        {
            try
            {
                var query = new GetAllAdditionalCostsQuery();
                var AdditionalCosts = await _mediator.Send(query);
                return Ok(AdditionalCosts);
            }
            catch (AdditionalCostNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search AdditionalCosts: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search AdditionalCosts");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdditionalCost([FromRoute] Guid id)
        {
            try
            {
                var command = new GetAdditionalCostQuery(id);
                var AdditionalCost = await _mediator.Send(command);
                return Ok(AdditionalCost);
            }
            catch (AdditionalCostNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an AdditionalCost: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an AdditionalCost");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAdditionalCost([FromRoute] Guid id, [FromBody] UpdateAdditionalCostDto updateAdditionalCostDto)
        {
            try
            {
                var command = new UpdateAdditionalCostCommand(id, updateAdditionalCostDto);
                var AdditionalCostId = await _mediator.Send(command);
                return Ok(AdditionalCostId);
            }
            catch (AdditionalCostNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an AdditionalCost: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an AdditionalCost");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAdditionalCost([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteAdditionalCostCommand(id);
                var AdditionalCostId = await _mediator.Send(command);
                return Ok(AdditionalCostId);
            }
            catch (AdditionalCostNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an AdditionalCost: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an AdditionalCost: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an AdditionalCost");
            }
        }

    }
}