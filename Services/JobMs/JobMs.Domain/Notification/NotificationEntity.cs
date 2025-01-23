using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMs.Common.Primitives;

namespace JobMs.Domain.Notification
{
    public class NotificationEntity : AggregateRoot
    {
        public Guid Id {get; set;}
        public string? DeviceToken{get; set;}
    }
}