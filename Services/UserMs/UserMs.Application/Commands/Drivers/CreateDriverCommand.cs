using UserMs.Application.Dtos.Drivers.Request;
using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Commands.Drivers
{
    public class CreateDriverCommand : IRequest<UserId>
    {
        public CreateDriverDto? Driver { get; set; }

        public CreateDriverCommand(CreateDriverDto? driver)
        {
            Driver = driver;
        }
    }
}