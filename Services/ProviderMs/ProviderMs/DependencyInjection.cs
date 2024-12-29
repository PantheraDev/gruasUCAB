using ProviderMs.Core.Database;
using ProviderMs.Core.Repository;
using ProviderMs.Infrastructure.Database;
using ProviderMs.Infrastructure.Repositories;

namespace ProviderMs
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
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IDepartamentRepository, DepartamentRepository>();
            services.AddScoped<IProviderDepartamentRepository, ProviderDepartamentRepository>();
            services.AddScoped<ITowRepository, TowRepository>();
            return services;
        }
    }
}