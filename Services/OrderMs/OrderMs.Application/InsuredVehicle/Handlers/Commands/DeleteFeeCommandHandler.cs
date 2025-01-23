using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Primitives;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class DeleteInsuredVehicleCommandHandler : IRequestHandler<DeleteInsuredVehicleCommand, Guid>
    {
        private readonly IInsuredVehicleRepository _insuredVehicleRepository;
        public DeleteInsuredVehicleCommandHandler(IInsuredVehicleRepository insuredVehicleRepository)
        {
            _insuredVehicleRepository = insuredVehicleRepository ?? throw new ArgumentNullException(nameof(insuredVehicleRepository)); //*Valido que estas inyecciones sean exitosas
        }
        public async Task<Guid> Handle(DeleteInsuredVehicleCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var insuredVehicleId = InsuredVehicleId.Create(request.InsuredVehicleId);
                await _insuredVehicleRepository.DeleteAsync(insuredVehicleId!);
                return insuredVehicleId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}