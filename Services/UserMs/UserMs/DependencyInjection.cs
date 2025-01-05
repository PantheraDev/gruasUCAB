
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using UserMs.Core.Interface;
using UserMs.Infrastructure;

namespace UserMs
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection KeycloakConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Audience = configuration["Authentication:Audience"];
                    options.MetadataAddress = configuration["Authentication:MetadataAddress"]!;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["Authentication:ValidateIssuer"],
                    };
                });

            services.AddAuthorization(o =>
            {
                // o.DefaultPolicy = new AuthorizationPolicyBuilder()
                //     .RequireAuthenticatedUser()
                //     .RequireClaim("email_verified", "true")
                //     .Build();

                o.AddPolicy("AdminProviderOnly", policy =>
         policy.RequireAuthenticatedUser()
               .RequireAssertion(context =>
               {
                   var resourceAccess = context.User.FindFirst("resource_access")?.Value;
                   if (string.IsNullOrEmpty(resourceAccess))
                       return false;

                   // Parsear el JSON de resource_access
                   var resourceAccessJson = System.Text.Json.JsonDocument.Parse(resourceAccess);
                   if (resourceAccessJson.RootElement.TryGetProperty("webclient", out var webClientAccess))
                   {
                       var rol = webClientAccess.GetProperty("roles").EnumerateArray()
                                             .Any(role => role.GetString() == "Administrator" || role.GetString() == "Provider");
                       Console.WriteLine("Rol:" + rol);
                       return rol;
                   }

                   return false;
               }));

                o.AddPolicy("AdminOnly", policy =>
          policy.RequireAuthenticatedUser()
                .RequireAssertion(context =>
                {
                    var resourceAccess = context.User.FindFirst("resource_access")?.Value;
                    if (string.IsNullOrEmpty(resourceAccess))
                        return false;

                    // Parsear el JSON de resource_access
                    var resourceAccessJson = System.Text.Json.JsonDocument.Parse(resourceAccess);
                    if (resourceAccessJson.RootElement.TryGetProperty("webclient", out var webClientAccess))
                    {
                        var rol = webClientAccess.GetProperty("roles").EnumerateArray()
                                              .Any(role => role.GetString() == "Administrator");
                        return rol;
                    }

                    return false;
                }));

                o.AddPolicy("AdminProviderOperatorOnly", policy =>
           policy.RequireAuthenticatedUser()
                 .RequireAssertion(context =>
                 {
                     var resourceAccess = context.User.FindFirst("resource_access")?.Value;
                     if (string.IsNullOrEmpty(resourceAccess))
                         return false;

                     // Parsear el JSON de resource_access
                     var resourceAccessJson = System.Text.Json.JsonDocument.Parse(resourceAccess);
                     if (resourceAccessJson.RootElement.TryGetProperty("webclient", out var webClientAccess))
                     {
                         var rol = webClientAccess.GetProperty("roles").EnumerateArray()
                                               .Any(role => role.GetString() == "Administrator" || role.GetString() == "Operator" || role.GetString() == "Provider");
                         return rol;
                     }

                     return false;
                 }));


            });
            services.AddHttpContextAccessor();
            services.AddHttpClient<IAuthMsService, AuthMsService>();


            return services;
        }
    }
}