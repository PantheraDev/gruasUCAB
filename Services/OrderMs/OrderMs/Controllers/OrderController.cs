
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
    [Route("order")]

    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminOperatorOnly")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                var command = new CreateOrderCommand(createOrderDto);
                var OrderId = await _mediator.Send(command);
                return Ok(OrderId);
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
            catch (ValidatorException e)
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

        [Authorize(Policy = "AdminDriverOperatorOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var query = new GetAllOrdersQuery();
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            try
            {
                var command = new GetOrderQuery(id);
                var Order = await _mediator.Send(command);
                return Ok(Order);
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
                _logger.LogError("An error occurred while trying to search an Order: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an Order");
            }
        }

        [Authorize(Policy = "AdminOperatorOnly")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromBody] UpdateOrderDto updateOrderDto)
        {
            try
            {
                var command = new UpdateOrderCommand(id, updateOrderDto);
                var OrderId = await _mediator.Send(command);
                return Ok(OrderId);
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
                _logger.LogError("An error occurred while trying to update an Order: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Order");
            }
        }

        [Authorize(Policy = "AdminOperatorOnly")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteOrderCommand(id);
                var OrderId = await _mediator.Send(command);
                return Ok(OrderId);
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
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an Order: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Order");
            }
        }

    }
}