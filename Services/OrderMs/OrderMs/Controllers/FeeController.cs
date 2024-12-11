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
    [Route("/fee")]

    public class FeeController : ControllerBase
    {
        private readonly ILogger<FeeController> _logger;
        private readonly IMediator _mediator;

        public FeeController(ILogger<FeeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> CreateFee([FromBody] CreateFeeDto createFeeDto)
        {
            try
            {
                var command = new CreateFeeCommand(createFeeDto);
                var FeeId = await _mediator.Send(command);
                return Ok(FeeId);
            }
            catch (FeeNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (ValidatorException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Fee");
            }
        }

        [Authorize(Policy = "AdminOperatorOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllFees()
        {
            try
            {
                var query = new GetAllFeesQuery();
                var Fees = await _mediator.Send(query);
                return Ok(Fees);
            }
            catch (FeeNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search Fees: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search Fees");
            }
        }

        [Authorize(Policy = "AdminOperatorOnly")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFee([FromRoute] Guid id)
        {
            try
            {
                var command = new GetFeeQuery(id);
                var Fee = await _mediator.Send(command);
                return Ok(Fee);
            }
            catch (FeeNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an Fee: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an Fee");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateFee([FromRoute] Guid id, [FromBody] UpdateFeeDto updateFeeDto)
        {
            try
            {
                var command = new UpdateFeeCommand(id, updateFeeDto);
                var FeeId = await _mediator.Send(command);
                return Ok(FeeId);
            }
            catch (FeeNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an Fee: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Fee");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteFee([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteFeeCommand(id);
                var FeeId = await _mediator.Send(command);
                return Ok(FeeId);
            }
            catch (FeeNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Fee: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an Fee: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Fee");
            }
        }

    }
}