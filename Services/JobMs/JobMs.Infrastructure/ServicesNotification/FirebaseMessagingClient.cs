using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMs.Common.Interface;
using Microsoft.Extensions.DependencyInjection;
using FirebaseAdmin.Messaging;

namespace JobMs.Infrastructure.ServicesNotification
{
    public class FirebaseMessagingClient : IFirebaseMessagingClient
    {
        public  async Task SendAsync(Message message)
        {
            await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }
        
    
}
}