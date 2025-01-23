
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderMs.ApplicationQueries;
using OrderMs.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using OrderMs.Application.Tow.Commands;

namespace OrderMs.Controllers
{
    [ApiController]
    [Route("order/tow")]

    public class TowController : ControllerBase
    {
        private readonly ILogger<TowController> _logger;
        private readonly IMediator _mediator;

        public TowController(ILogger<TowController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }



        [Authorize(Policy = "AdminDriverOperatorOnly")]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetTowsAvailable([FromRoute] Guid orderId)
        {
            try
            {
                var query = new GetTowsAvailableQuery(orderId);
                var orders = await _mediator.Send(query);
                return Ok(orders);
            }
            catch (OrderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search Orders: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search Orders");
            }
        }

        [Authorize(Policy = "AdminDriverOperatorOnly")]
        [HttpPost("addTow/{orderId}/{towId}")]
        public async Task<IActionResult> AddTowToOrder([FromRoute] Guid orderId, [FromRoute] Guid towId)
        {
            try
            {
                var command = new AddTowToOrderCommand(orderId, towId);
                var order = await _mediator.Send(command);
                return Ok(order);
            }
            catch (OrderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Order");
            }
        }


        //[Authorize(Policy = "AdminDriverOperatorOnly")]
        [HttpPut("rejectOrder/{orderId}")]
        public async Task<IActionResult> RejectOrder([FromRoute] Guid orderId)
        {
            try
            {
                var command = new RejectOrderCommand(orderId);
                var order = await _mediator.Send(command);
                return Ok(order);
            }
            catch (OrderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Order");
            }
        }
        //[Authorize(Policy = "AdminDriverOperatorOnly")]
        [HttpPut("acceptOrder/{orderId}")]
        public async Task<IActionResult> AcceptOrder([FromRoute] Guid orderId)
        {
            try
            {
                var command = new AcceptOrderCommand(orderId);
                var order = await _mediator.Send(command);
                return Ok(order);
            }
            catch (OrderNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an Order: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Order");
            }
        }
    }
}