using MediatR;
using ProviderMs.Domain.ValueObjects;
using ProviderMs.Application.Validators;
using ProviderMs.Common.Exceptions;
using ProviderMs.Common.Primitives;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using ProviderMs.Common.Enums;
using ProviderMs.Core.Services;


namespace ProviderMs.Application.Command
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class AddDriverCommandHandler : IRequestHandler<AddDriverCommand, Guid>
    {
        private readonly ITowRepository _VehicleRepository;
        private readonly IUserService _UserService;
        public AddDriverCommandHandler(ITowRepository VehicleRepository, IUserService UserService)
        {
            _VehicleRepository = VehicleRepository ?? throw new ArgumentNullException(nameof(VehicleRepository)); //*Valido que estas inyecciones sean exitosas
            _UserService = UserService ?? throw new ArgumentNullException(nameof(UserService));
        }

        public async Task<Guid> Handle(AddDriverCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldTow = await _VehicleRepository.GetByIdAsync(VehicleId.Create(request.TowId)!);

                if (oldTow == null) throw new VehicleNotFoundException("Tow not found");

                if (!await _UserService.DriverExists(TowDriver.Create(request.DriverId)!)) throw new NotFoundException("Driver not found");

                oldTow = Tow.Update(oldTow, null, null, null, null, null, null, null, null, null, TowDriver.Create(request.DriverId));

                await _VehicleRepository.UpdateAsync(oldTow);

                return oldTow.Id.Value;
            }
            catch
            {
                throw;
            }

        }
    }
}