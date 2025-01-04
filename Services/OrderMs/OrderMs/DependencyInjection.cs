
using System.Net.Http.Headers;
using OrderMs.Core.Database;
using OrderMs.Core.Repositories;
using OrderMs.Core.Services;
using OrderMs.Infrastructure;
using OrderMs.Infrastructure.Database;
using OrderMs.Infrastructure.Repositories;
using OrderMs.Infrastructure.Services;
using OrderMS;


namespace OrderMs
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
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IFeeRepository, FeeRepository>();
            services.AddScoped<IIncidentRepository, IncidentRepository>();
            services.AddScoped<IAdditionalCostRepository, AdditionalCostRepository>();
            services.AddScoped<IInsuredVehicleRepository, InsuredVehicleRepository>();
            services.AddScoped<IPolicyRepository, PolicyRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IGoogleApiService, GoogleApiService>();

            services.AddScoped<ITowRepository, TowService>();
            services.AddScoped<INotificationRepository, NotificationService>();

            services.AddHttpContextAccessor();


            services.AddHttpClient<TowService>(
                client =>
                {
                    client.BaseAddress = new Uri("https://localhost:18083");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
            );
            services.AddHttpClient<NotificationService>(
                client =>
                {
                    client.BaseAddress = new Uri("https://localhost:18085");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
            );
            return services;
        }
    }
}