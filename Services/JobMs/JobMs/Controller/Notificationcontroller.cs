using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using ProviderMs.Application.Handlers.Queries;
//using ProviderMs.Application.Command;
using JobMs.Common.Dto;
using FirebaseAdmin.Messaging;
using JobMs.Common.Interface;
using JobMs.Application.Notification.Command;
//using ProviderMs.Domain.Entities;
//using ProviderMs.Application.Queries;
//using ProviderMs.Common.Exceptions;
//using ProviderMs.Infrastructure.Exceptions;
//using ProviderMs.ApplicationQueries;

namespace JobMs.Controllers
{
    [ApiController]
    [Route("job/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<FirebaseController> _logger;
        private readonly IMediator _mediator;
        private readonly IFirebaseMessagingServices _firebaseMessagingServices;

        public NotificationController(ILogger<FirebaseController> logger, IMediator mediator, IFirebaseMessagingServices firebaseMessagingServices)
        {
            _logger = logger;
            _mediator = mediator;
            _firebaseMessagingServices = firebaseMessagingServices;
        }


        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] MessageRequestDto messageRequestDto)
        {
            try
            {
                var command = new CreateCommandNotification(messageRequestDto);
                var Notification = await _mediator.Send(command);
                return Ok(Notification);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
