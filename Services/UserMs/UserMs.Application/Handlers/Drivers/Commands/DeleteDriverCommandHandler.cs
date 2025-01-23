using UserMs.Application.Commands.Drivers;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Infrastructure.Exceptions;
using UserMs.Core.Interface;

namespace UserMs.Application.Handlers.Drives.Commands
{
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, UserId>
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IAuthMsService _authMsService;

        public DeleteDriverCommandHandler(IDriverRepository driverRepository, IAuthMsService authMsService)
        {
            _driverRepository = driverRepository;
            _authMsService = authMsService;
        }

        public async Task<UserId> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var driver = await _driverRepository.GetDriverById(request.UserId);

                await _authMsService.DeleteUserAsync(driver!.UserId);
                
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