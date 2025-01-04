using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMs.Application.Notification.Command;
using MediatR;
using JobMs.Core.Repository;
using System.Data.Common;
using System.ComponentModel;
using JobMs.Domain.Notification;

namespace JobMs.Application.Notification.Handler
{
    public class CreateCommandNotificationHandler: IRequestHandler<CreateCommandNotification, Guid>
    {
        private readonly INotificationFirebaseRepository _notificationRepository;

        public CreateCommandNotificationHandler(INotificationFirebaseRepository notificationRepository)
        {
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
        }

        public async Task<Guid> Handle (CreateCommandNotification request, CancellationToken cancellationToken)
        {
            
            var Notification = new NotificationEntity{
                Id = request.Notification.Id,
                DeviceToken = request.Notification.DeviceToken,
            };

            await _notificationRepository.AddAsync(Notification);

            return Notification.Id;
        }
    }
}