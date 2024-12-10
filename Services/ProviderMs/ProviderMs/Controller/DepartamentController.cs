using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProviderMs.Application.Handlers.Queries;
using ProviderMs.Application.Command;
using ProviderMs.Common.dto.Request;
using ProviderMs.Domain.Entities;
using ProviderMs.Application.Queries;
using ProviderMs.ApplicationQueries;
using ProviderMs.Common.Exceptions;
using ProviderMs.Infrastructure.Exceptions;

namespace ProviderMs.Controllers
{
    [ApiController]
    [Route("/Departament")]
    public class DepartamentController : ControllerBase
    {
        private readonly ILogger<DepartamentController> _logger;
        private readonly IMediator _mediator;

        public DepartamentController(ILogger<DepartamentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatedDepartament([FromBody] CreateDepartamentdto createDepartamentdto)
        {
            try
            {
                var command = new CreateDepartamentCommand(createDepartamentdto);
                var DepartamentId = await _mediator.Send(command);
                return Ok(DepartamentId);
            }
            catch (DepartamentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (ValidatorException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Departament");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartaments()
        {
            try
            {
                var query = new GetAllDepartamentsQuery();
                var Departaments = await _mediator.Send(query);
                return Ok(Departaments);
            }
            catch (DepartamentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search Departament: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search Departament");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartament([FromRoute] Guid id)
        {
            try
            {
                var command = new GetDepartamentQuery(id);
                var Departament = await _mediator.Send(command);
                return Ok(Departament);
            }
            catch (DepartamentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to search an Departament: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to search an Departament");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDepartament([FromRoute] Guid id, [FromBody] UpdateDepartamentDto updateDepartamentDto)
        {
            try
            {
                var command = new UpdateDepartamentCommand(id, updateDepartamentDto);
                var DepartamentId = await _mediator.Send(command);
                return Ok(DepartamentId);
            }
            catch (DepartamentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to update an Departament: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Departament");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDepartament([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteDepartamentCommand(id);
                var DepartamentId = await _mediator.Send(command);
                return Ok(DepartamentId);
            }
            catch (DepartamentNotFoundException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(404, e.Message);
            }
            catch (NullAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (InvalidAttributeException e)
            {
                _logger.LogError("An error occurred while trying to create an Departament: {Message}", e.Message);
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                //TODO: Colocar validaciones HTTP
                _logger.LogError("An error occurred while trying to delete an Departament: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Departament");
            }
        }

    }
}