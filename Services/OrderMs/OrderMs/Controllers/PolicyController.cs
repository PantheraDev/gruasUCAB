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
    [Route("/policy")]
    public class PolicyController : ControllerBase
    {
        private readonly ILogger<PolicyController> _logger;
        private readonly IMediator _mediator;

        public PolicyController(ILogger<PolicyController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePolicy([FromBody] CreatePolicyDto createPolicyDto)
        {
            try
            {
                var command = new CreatePolicyCommand(createPolicyDto);
                var PolicyId = await _mediator.Send(command);
                return Ok(PolicyId);
            }
            catch (PolicyNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (ValidatorException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Policy");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPolicys()
        {
            try
            {
                var query = new GetAllPolicysQuery();
                var Policys = await _mediator.Send(query);
                return Ok(Policys);
            }
            catch (PolicyNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search Policys: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search Policys");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicy([FromRoute] Guid id)
        {
            try
            {
                var command = new GetPolicyQuery(id);
                var Policy = await _mediator.Send(command);
                return Ok(Policy);
            }
            catch (PolicyNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an Policy: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an Policy");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePolicy([FromRoute] Guid id, [FromBody] UpdatePolicyDto updatePolicyDto)
        {
            try
            {
                var command = new UpdatePolicyCommand(id, updatePolicyDto);
                var PolicyId = await _mediator.Send(command);
                return Ok(PolicyId);
            }
            catch (PolicyNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an Policy: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Policy");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePolicy([FromRoute] Guid id)
        {
            try
            {
                var command = new DeletePolicyCommand(id);
                var PolicyId = await _mediator.Send(command);
                return Ok(PolicyId);
            }
            catch (PolicyNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Policy: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an Policy: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Policy");
            }
        }

    }
}