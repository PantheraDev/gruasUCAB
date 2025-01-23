
using AuthenticationMs.Core;
using AuthenticationMs.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthenticationMs.Common.Exceptions;

namespace AuthenticationMs
{

    [ApiController]
    [Route("auth")]


    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IKeycloakRepository _keycloakRepository;

        public AuthController(ILogger<AuthController> logger, IKeycloakRepository keycloakRepository)
        {
            _logger = logger;
            _keycloakRepository = keycloakRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _keycloakRepository.LoginAsync(loginDto.Username, loginDto.Password);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOutAsync()
        {
            try
            {
                var result = await _keycloakRepository.LogOutAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error");
                return StatusCode(500, ex.Message);
            }
        }

        // [Authorize(Policy = "AdminOnly")]
        // [HttpPost("token")]
        // public async Task<IActionResult> GetTokenAsync()
        // {
        //     try
        //     {
        //         var token = await _keycloakRepository.GetTokenAsync();
        //         return Ok(token);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Controller error");
        //         return StatusCode(500, ex.Message);
        //     }
        // }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto user)
        {
            try
            {
                var token = await _keycloakRepository.CreateUserAsync(user);
                return Ok(token);
            }
            catch (UserExistException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(409, ex.Message);
            }
            catch (KeycloakException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpDelete]
        [Route("deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId)
        {
            try
            {
                var token = await _keycloakRepository.DeleteUserAsync(new Guid(userId.ToString()));
                return Ok(token);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
            catch (KeycloakException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUserNameAsync([FromRoute] string username)
        {
            try
            {
                Console.WriteLine("userName: " + username);
                var token = await _keycloakRepository.GetUserByUserName(username);
                return Ok(token);
                //TODO: USER NOt FOUND
            }
            catch (KeycloakException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Policy = "AdminProviderOnly")]
        [HttpPost("assingRole")]
        public async Task<IActionResult> AssingRoleAsync([FromBody] AssingRoleDto role)
        {
            try
            {
                await _keycloakRepository.AssignClientRoleToUser(role.UserId, role.ClientId, role.RoleName);
                // if (role.ClientId == "webclient") await _keycloakRepository.AssignClientRoleToUser(role.UserId, role.ClientId, role.RoleName);
                // else await _keycloakRepository.AssignClientRoleToUserMobile(role.UserId, role.ClientId, role.RoleName);
                return Ok();
            }
            catch (KeycloakException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Policy = "AdminProviderOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> AssingRoleAsync([FromRoute] Guid id, [FromBody] UpdateUserDto user)
        {
            try
            {
                await _keycloakRepository.UpdateUser(id, user);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
            catch (UserExistException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(409, ex.Message);
            }
            catch (KeycloakException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error");
                return StatusCode(500, ex.Message);
            }
        }
    }
}