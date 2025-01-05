using System.Security.Claims;
using AuthenticationMs;
using AuthenticationMs.Core;
using AuthenticationMs.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth(builder.Configuration);
builder.Services.KeycloakConfiguration(builder.Configuration);


builder.Services.AddScoped<IKeycloakRepository, KeycloakRepository>();

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("users/me", (ClaimsPrincipal user) =>
{
    return user.Claims.ToDictionary(c => c.Type, c => c.Value);
}).RequireAuthorization();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
