using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MediatR;
using JobMs.Core.Repository;
using JobMs.Domain.Notification;
using Microsoft.Build.Framework;
using FirebaseAdmin.Messaging;
using JobMs.Common.Interface;
using JobMs.Infrastructure.Settings;
using JobMs.Infrastructure.Database;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace JobMs.Infrastructure.ServicesNotification
{
    public class SendPushNotificationAsync : IFirebaseMessagingServices
    {

        //private readonly ILogger<SendPushNotificationAsync> _logger;
        private readonly IFirebaseMessagingClient _firebaseMessagingClient;
        private readonly INotificationFirebaseRepository _notificationFirebaseRepository;
       

        public SendPushNotificationAsync(IFirebaseMessagingClient firebaseMessagingClient /*ILogger logger*/, INotificationFirebaseRepository notificationFirebaseRepository)
        {
            _firebaseMessagingClient = firebaseMessagingClient;
            _notificationFirebaseRepository = notificationFirebaseRepository;
            //_logger = logger;
        }

        public async Task SendNotificationAsync(Guid id)
        {
            try {

                var  UserToken = await _notificationFirebaseRepository.GetByUserIdAsync(id);
                Console.WriteLine("aqui esta el token");
                Console.WriteLine(UserToken);
                if(UserToken == null)
                {
                    throw new Exception("User not found or not have a devicetoken");
                }
                var message = new Message()
                {
                    Token = UserToken.DeviceToken,
                    Notification = new Notification(){
                        Title = "Se ha creado una nueva orden",
                        Body = "Se te ha asignado una nueva orden de grua"
                    },
                };

                await _firebaseMessagingClient.SendAsync(message);

                /*var existingUserToken = await _notificationFirebaseRepository.GetByUserIdAsync(id);
                if(existingUserToken == null){
                    var userToken = new NotificationEntity
                    {
                        Id = messageRequestDto.Id,
                        DeviceToken = messageRequestDto.DeviceToken,
                    };
                }
                var message = new Message()
                {
                    Token = messageRequestDto.DeviceToken,
                    Notification = new Notification(){
                        Title = "Se ha creado una nueva orden",
                        Body = "Se te ha asignado una nueva orden de grua"
                    },
                };
                await _firebaseMessagingClient.SendAsync(message);*/
            }
            catch (Exception ex){
                throw;
            }
        }
       
}
}