
using ProviderMs.Core.Database;
using ProviderMs.Core.Repository;
using ProviderMs.Infrastructure.Database;
using ProviderMs.Infrastructure.Repositories;

namespace ProviderMs
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
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IProviderDepartmentRepository, ProviderDepartmentRepository>();
            services.AddScoped<ITowRepository, TowRepository>();
            return services;
        }
    }
}