using UserMs.Application.Dtos.Drivers.Request;
using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Commands.Drivers
{
    public class UpdateDriverCommand : IRequest<Driver>
    {
        public UserId UserId { get; set; }
        public UpdateDriverDto Driver { get; set; }

        public UpdateDriverCommand(UserId userId, UpdateDriverDto driver)
        {
            UserId = userId;
            Driver = driver;
        }
    }
}