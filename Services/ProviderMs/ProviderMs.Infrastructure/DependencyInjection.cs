using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProviderMs.Common.Primitives;
using ProviderMs.Core.Database;
using ProviderMs.Core.Repository;
using ProviderMs.Infrastructure.Database;
using ProviderMs.Infrastructure.Repositories;

namespace ProviderMs.Infrastructure
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
            var BusinessConnectionString = Environment.GetEnvironmentVariable("DB_BUSINESS_CONNECTION_STRING") ??
            configuration.GetConnectionString("BusinessPostgreSQLConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(BusinessConnectionString));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>()!);
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>()!);

            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<TowRepository, TowRepository>();

            return services;
        }
    }
}