using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobMs.Common.Dto;
using FirebaseAdmin.Messaging;
using JobMs.Common.Interface;
using JobMs.Application.Notification.Command;


namespace JobMs.Controllers
{
    [ApiController]
    [Route("job/firebase")]
    public class FirebaseController : ControllerBase
    {
        private readonly ILogger<FirebaseController> _logger;
        private readonly IMediator _mediator;
        private readonly IFirebaseMessagingServices _firebaseMessagingServices;

        public FirebaseController(ILogger<FirebaseController> logger, IMediator mediator, IFirebaseMessagingServices firebaseMessagingServices)
        {
            _logger = logger;
            _mediator = mediator;
            _firebaseMessagingServices = firebaseMessagingServices;
        }

        [HttpPost]
        public async Task<ActionResult> SendNotification ([FromBody] Guid id)
        {
            await _firebaseMessagingServices.SendNotificationAsync(id);
            return Ok();
        }

        

        /*[HttpPost]
        public async Task<IActionResult> SendNotificationMesagge([FromBody] MessageRequestDto messageRequestDto)
        {
            var message = new Message {
                Notification = new Notification
                {
                    Title = messageRequestDto.Title,
                    Body = messageRequestDto.Body
                },
                Token = messageRequestDto.DeviceToken
            };
            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);

            if (!string.IsNullOrEmpty(response))
                 {
                         // Message was sent successfully
                         return Ok("Message sent successfully!");
                 }
                 else
                     {
                        // There was an error sending the message
                        throw new Exception("Error sending the message.");
                     }
        }*/

    }
}