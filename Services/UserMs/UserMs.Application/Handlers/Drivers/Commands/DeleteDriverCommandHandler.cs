using UserMs.Application.Commands.Drivers;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Application.Handlers.Drives.Commands
{
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, UserId>
    {
        private readonly IDriverRepository _driverRepository;

        public DeleteDriverCommandHandler(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<UserId> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var driver = await _driverRepository.GetDriverById(request.UserId);
                
                driver.SetUserDelete(UserDelete.Create(true));

                await _driverRepository.DeleteDriverAsync(request.UserId);

                return request.UserId;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}