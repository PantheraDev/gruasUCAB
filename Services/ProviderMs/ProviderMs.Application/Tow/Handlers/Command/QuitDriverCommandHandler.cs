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
    public class QuitDriverCommandHandler : IRequestHandler<QuitDriverCommand, string>
    {
        private readonly ITowRepository _VehicleRepository;
        public QuitDriverCommandHandler(ITowRepository VehicleRepository, IUserService UserService)
        {
            _VehicleRepository = VehicleRepository ?? throw new ArgumentNullException(nameof(VehicleRepository));
        }

        public async Task<string> Handle(QuitDriverCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldTow = await _VehicleRepository.GetByIdAsync(VehicleId.Create(request.TowId)!);

                if (oldTow == null) throw new VehicleNotFoundException("Tow not found");

                if (oldTow.TowDriver == null) return "Tow has no driver";
                else{
                    oldTow.TowDriver = null;
                    await _VehicleRepository.UpdateAsync(oldTow);
                }
                
                return "Driver has been removed from tow";
            }
            catch
            {
                throw;
            }

        }

    }
}