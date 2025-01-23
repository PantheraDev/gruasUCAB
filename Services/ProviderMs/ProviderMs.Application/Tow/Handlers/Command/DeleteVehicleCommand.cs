using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Application.Validators;
using ProviderMs.Common.Primitives;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class DeleteTowCommandHandler : IRequestHandler<DeleteTowCommand, Guid>
    {
        private readonly ITowRepository _VehicleRepository;
        public DeleteTowCommandHandler(ITowRepository VehicleRepository)
        {
            _VehicleRepository = VehicleRepository ?? throw new ArgumentNullException(nameof(VehicleRepository)); //*Valido que estas inyecciones sean exitosas
        }
        public async Task<Guid> Handle(DeleteTowCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var towId = VehicleId.Create(request.VehicleId);
                await _VehicleRepository.DeleteAsync(towId!);
                return towId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}