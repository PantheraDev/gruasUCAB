using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JobMs.Core.Database;
using JobMs.Core.Repository;
using JobMs.Domain.Notification;

namespace JobMs.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationFirebaseRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public NotificationRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(NotificationEntity notificationEntity)
        {
            await _dbContext.Notification.AddAsync(notificationEntity);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<NotificationEntity?> GetByUserIdAsync(Guid id)
        {
            return await _dbContext.Notification.FirstOrDefaultAsync(x => x.Id == id);
        }

}
}