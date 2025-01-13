using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProviderMs;
using ProviderMs.Application;
using ProviderMs.Application.Command;
using ProviderMs.Core.Database;
using ProviderMs.Core.Services;
using ProviderMs.Infrastructure;
using ProviderMs.Infrastructure.Database;
using ProviderMs.Infrastructure.Services;
using ProviderMs.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddPresentation(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

//* Para que funcione el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var _appSettings = new AppSettings();
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
_appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();