using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderMs.Application.Handlers.Queries;
using OrderMs.Application.Commands;
using OrderMs.Common.Dtos.Request;
using OrderMs.Domain.Entities;
using OrderMs.Application.Queries;
using OrderMs.ApplicationQueries;
using OrderMs.Common.Exceptions;
using OrderMs.Infrastructure.Exceptions;

namespace OrderMs.Controllers
{
    [ApiController]
    [Route("/client")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;

        public ClientController(ILogger<ClientController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto createClientDto)
        {
            try
            {
                var command = new CreateClientCommand(createClientDto);
                var clientId = await _mediator.Send(command);
                return Ok(clientId);
            }
            catch (ClientNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (ValidatorException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an client");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var query = new GetAllClientsQuery();
                var clients = await _mediator.Send(query);
                return Ok(clients);
            }
            catch (ClientNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search clients: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search clients");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient([FromRoute] Guid id)
        {
            try
            {
                var command = new GetClientQuery(id);
                var client = await _mediator.Send(command);
                return Ok(client);
            }
            catch (ClientNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an client: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an client");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateClient([FromRoute] Guid id, [FromBody] UpdateClientDto updateClientDto)
        {
            try
            {
                var command = new UpdateClientCommand(id, updateClientDto);
                var clientId = await _mediator.Send(command);
                return Ok(clientId);
            }
            catch (ClientNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an client: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an client");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteClientCommand(id);
                var clientId = await _mediator.Send(command);
                return Ok(clientId);
            }
            catch (ClientNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an client: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an client: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an client");
            }
        }

    }
}