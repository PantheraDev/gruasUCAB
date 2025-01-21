using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMs.Common.Dto;
using MediatR;

namespace JobMs.Application.Notification.Command
{
    public class CreateCommandNotification :IRequest<Guid>
    {
        public MessageRequestDto Notification { get; set; }

        public CreateCommandNotification(MessageRequestDto notification)
        {
            Notification = notification;
        }
    }
}