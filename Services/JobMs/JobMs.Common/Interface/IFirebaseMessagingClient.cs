using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;

namespace JobMs.Common.Interface
{
    public interface IFirebaseMessagingClient
    {
        Task SendAsync (Message message);
    }
}