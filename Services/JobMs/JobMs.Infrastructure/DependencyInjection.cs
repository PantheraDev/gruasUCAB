using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JobMs.Common.Primitives;
using JobMs.Core.Database;
using JobMs.Core.Repository;
using JobMs.Infrastructure.Database;
using JobMs.Infrastructure.Repositories;
using JobMs.Common.Interface;
using JobMs.Infrastructure.ServicesNotification;

namespace JobMs.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            var BusinessConnectionString = Environment.GetEnvironmentVariable("DB_BUSINESS_CONNECTION_STRING")??
                configuration.GetConnectionString("BusinessPostgresSQLConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(BusinessConnectionString));
            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
           //     options.UseNpgsql(configuration.GetConnectionString("PostgresSQLConnection"));
           // });
            services.AddScoped<IApplicationDbContext>(notification => notification.GetRequiredService<ApplicationDbContext>()!);
            services.AddScoped<IUnitOfWork>(notification => notification.GetRequiredService<ApplicationDbContext>()!);

            services.AddScoped<INotificationFirebaseRepository, NotificationRepository>();
            services.AddScoped<IFirebaseMessagingServices, SendPushNotificationAsync>();
            return services;
        }
    }
}