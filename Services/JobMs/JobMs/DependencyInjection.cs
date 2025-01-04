using JobMs.Common.Interface;
using JobMs.Core.Database;
using JobMs.Core.Repository;
using JobMs.Infrastructure.Database;
using JobMs.Infrastructure.Repositories;
using JobMs.Infrastructure.ServicesNotification;
using JobMs;

namespace JobMs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGenWithAuth(configuration);
            services.KeycloakConfiguration(configuration);

            //* Sin los Scope no funciona!!
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<INotificationFirebaseRepository, NotificationRepository>();
            services.AddScoped<IFirebaseMessagingServices, SendPushNotificationAsync>();
            services.AddScoped<IFirebaseMessagingClient, FirebaseMessagingClient>();
            return services;
        }
    }
}