
using OrderMs.Core.Database;
using OrderMs.Core.Repositories;
using OrderMs.Infrastructure.Database;
using OrderMs.Infrastructure.Repositories;

namespace OrderMs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //* Sin los Scope no funciona!!
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IFeeRepository, FeeRepository>();
            services.AddScoped<IIncidentRepository, IncidentRepository>();
            services.AddScoped<IAdditionalCostRepository, AdditionalCostRepository>();
            services.AddScoped<IInsuredVehicleRepository, InsuredVehicleRepository>();
            services.AddScoped<IPolicyRepository, PolicyRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}