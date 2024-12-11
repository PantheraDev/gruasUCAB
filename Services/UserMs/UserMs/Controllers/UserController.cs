using UserMs.Application.Commands.User;
using UserMs.Application.Queries.User;
using UserMs.Application.Dtos.Users.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserMs.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace UserMs.Controllers
{

    [ApiController]
    [Route("user/users")]
    [Authorize(Policy = "AdminOnly")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsers(CreateUsersDto createUsersDto)
        {
            try
            {
                var command = new CreateUsersCommand(createUsersDto);
                var userId = await _mediator.Send(command);
                return Ok(userId);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an User {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an User");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var query = new GetUsersQuery();
                var users = await _mediator.Send(query);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while getting User {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting User");
            }
        }

        [HttpGet("{usersId}")]
        public async Task<IActionResult> GetUsersById([FromRoute] Guid usersId)
        {
            try
            {
                UserId userId = UserId.Create(usersId);
                var query = new GetUsersByIdQuery(userId);
                var users = await _mediator.Send(query);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while getting one User {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting one User");
            }
        }

        [HttpPut("{usersId}")]
        public async Task<IActionResult> UpdateUsers([FromRoute] Guid usersId, [FromBody] UpdateUsersDto usersDto)
        {
            try
            {
                UserId userId = UserId.Create(usersId);
                var command = new UpdateUsersCommand(userId, usersDto);
                var users = await _mediator.Send(command);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an User {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an User");
            }
        }

        [HttpDelete("{usersId}")]
        public async Task<IActionResult> DeleteUsers(Guid usersId)
        {
            try
            {
                UserId userId = UserId.Create(usersId);
                var command = new DeleteUsersCommand(userId);
                var users = await _mediator.Send(command);
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an User {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an User");
            }
        }
    }
}