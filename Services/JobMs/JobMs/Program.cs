using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JobMs;
using JobMs.Application;
//using JobMs.Application.Command;
using JobMs.Core.Database;
using JobMs.Infrastructure;
using JobMs.Infrastructure.Database;
using JobMs.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile( "notification-27cdc-firebase-adminsdk-6lhrr-52ff625b29.json"),
});

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Connected!");

app.Run();
