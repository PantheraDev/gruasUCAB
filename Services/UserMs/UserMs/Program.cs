using UserMs.Application.Handlers.Drives.Commands;
using UserMs.Application.Handlers.User.Commands;
using UserMs.Application.Handlers.License.Commands;
using UserMs.Core.Database;
using UserMs.Core.Repositories;
using UserMs.Infrastructure.Database;
using UserMs.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserMs.Application.Handlers.Drives.Queries;
using UserMs.Application.Handlers.User.Queries;
using UserMs.Application.Handlers.License.Queries;
using UserMs;
using UserMs.Core.Interface;
using UserMs.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth(builder.Configuration);
builder.Services.KeycloakConfiguration(builder.Configuration);

builder.Services.AddMediatR(typeof(CreateLicenseCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetLicenseQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetLicenseByIdQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateLicenseCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteLicenseCommandHandler).Assembly);

builder.Services.AddMediatR(typeof(CreateDriverCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetDriverQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetDriverByIdQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateDriverCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteDriverCommandHandler).Assembly);

builder.Services.AddMediatR(typeof(CreateUsersCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetUsersQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetUsersByIdQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateUsersCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteUsersCommandHandler).Assembly);

builder.Services.AddTransient<ILicenseRepository, LicenseRepository>();
builder.Services.AddTransient<IUserDbContext, UserDbContext>();
builder.Services.AddTransient<IDriverRepository, DriverRepository>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IAuthMsService, AuthMsService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new LicenseIdJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new LicenseDateExpirationJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new LicenseNumberJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new UserIdJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new UserEmailJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new UserPasswordJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new UserProviderJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new UserDepartamentJsonConverter());
});

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQLConnection")));

var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();