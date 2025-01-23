using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMs.Common.Dto;
using JobMs.Domain.Notification;

namespace JobMs.Core.Repository
{
    public interface INotificationFirebaseRepository
    {
        Task AddAsync(NotificationEntity notificationEntity);
        Task <NotificationEntity?> GetByUserIdAsync (Guid id);
    }
}