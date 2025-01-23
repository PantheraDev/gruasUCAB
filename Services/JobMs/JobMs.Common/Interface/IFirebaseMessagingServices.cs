using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMs.Common.Dto;

namespace JobMs.Common.Interface
{
    public interface IFirebaseMessagingServices
    {
        Task SendNotificationAsync(Guid id);
    }
}