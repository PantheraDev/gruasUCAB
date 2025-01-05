
using System.Net.Http.Headers;
using AuthenticationMs.Core;
using AuthenticationMs.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AuthenticationMs.Common.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace AuthenticationMs
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

                o.AddPolicy("AdminMobileOnly", policy =>
          policy.RequireAuthenticatedUser()
                .RequireAssertion(context =>
                {
                    var resourceAccess = context.User.FindFirst("resource_access")?.Value;
                    if (string.IsNullOrEmpty(resourceAccess))
                        return false;

                    // Parsear el JSON de resource_access
                    var resourceAccessJson = System.Text.Json.JsonDocument.Parse(resourceAccess);
                    if (resourceAccessJson.RootElement.TryGetProperty("mobileclient", out var webClientAccess))
                    {
                        return webClientAccess.GetProperty("roles").EnumerateArray()
                                              .Any(role => role.GetString() == "Administrator");
                    }

                    return false;
                }));

                //         o.AddPolicy("ProviderOnly", policy =>
                //   policy.RequireAuthenticatedUser()
                //         .RequireAssertion(context =>
                //         {
                //             var resourceAccess = context.User.FindFirst("resource_access")?.Value;
                //             if (string.IsNullOrEmpty(resourceAccess))
                //                 return false;

                //             // Parsear el JSON de resource_access
                //             var resourceAccessJson = System.Text.Json.JsonDocument.Parse(resourceAccess);
                //             if (resourceAccessJson.RootElement.TryGetProperty("webclient", out var webClientAccess))
                //             {
                //                 return webClientAccess.GetProperty("roles").EnumerateArray()
                //                                       .Any(role => role.GetString() == "Provider");
                //             }

                //             return false;
                //         }));

                // o.AddPolicy("OperatorOnly", policy =>
                // policy.RequireAuthenticatedUser()
                // .RequireAssertion(context =>
                // {
                //     var resourceAccess = context.User.FindFirst("resource_access")?.Value;
                //     if (string.IsNullOrEmpty(resourceAccess))
                //         return false;

                //     // Parsear el JSON de resource_access
                //     var resourceAccessJson = System.Text.Json.JsonDocument.Parse(resourceAccess);
                //     if (resourceAccessJson.RootElement.TryGetProperty("webclient", out var webClientAccess))
                //     {
                //         return webClientAccess.GetProperty("roles").EnumerateArray()
                //                               .Any(role => role.GetString() == "Operator");
                //     }

                //     return false;
                // }));

                // o.AddPolicy("DriverOnly", policy =>
                //     policy.RequireAuthenticatedUser()
                //           .RequireAssertion(context =>
                //           {
                //               var resourceAccess = context.User.FindFirst("resource_access")?.Value;
                //               if (string.IsNullOrEmpty(resourceAccess))
                //                   return false;

                //               // Parsear el JSON de resource_access
                //               var resourceAccessJson = System.Text.Json.JsonDocument.Parse(resourceAccess);
                //               if (resourceAccessJson.RootElement.TryGetProperty("webclient", out var webClientAccess))
                //               {
                //                   return webClientAccess.GetProperty("roles").EnumerateArray()
                //                                         .Any(role => role.GetString() == "Driver");
                //               }

                //               return false;
                //           }));
            });

            // Agrega IHttpContextAccessor (necesario para acceder al contexto HTTP)
            services.AddHttpContextAccessor();
            // Registra el DelegatingHandler

            // Configura HttpClient para IKeycloakRepository
            services.AddHttpClient<IKeycloakRepository, KeycloakRepository>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri("https://localhost:18080/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });


            return services;
        }
    }
}