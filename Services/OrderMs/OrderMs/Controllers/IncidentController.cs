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
    [Route("/incident")]
    public class IncidentController : ControllerBase
    {
        private readonly ILogger<IncidentController> _logger;
        private readonly IMediator _mediator;

        public IncidentController(ILogger<IncidentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncident([FromBody] CreateIncidentDto createIncidentDto)
        {
            try
            {
                var command = new CreateIncidentCommand(createIncidentDto);
                var IncidentId = await _mediator.Send(command);
                return Ok(IncidentId);
            }
            catch (IncidentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (ValidatorException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Incident");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIncidents()
        {
            try
            {
                var query = new GetAllIncidentsQuery();
                var Incidents = await _mediator.Send(query);
                return Ok(Incidents);
            }
            catch (IncidentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search Incidents: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search Incidents");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncident([FromRoute] Guid id)
        {
            try
            {
                var command = new GetIncidentQuery(id);
                var Incident = await _mediator.Send(command);
                return Ok(Incident);
            }
            catch (IncidentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an Incident: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an Incident");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateIncident([FromRoute] Guid id, [FromBody] UpdateIncidentDto updateIncidentDto)
        {
            try
            {
                var command = new UpdateIncidentCommand(id, updateIncidentDto);
                var IncidentId = await _mediator.Send(command);
                return Ok(IncidentId);
            }
            catch (IncidentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an Incident: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Incident");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteIncident([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteIncidentCommand(id);
                var IncidentId = await _mediator.Send(command);
                return Ok(IncidentId);
            }
            catch (IncidentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Incident: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an Incident: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Incident");
            }
        }

    }
}