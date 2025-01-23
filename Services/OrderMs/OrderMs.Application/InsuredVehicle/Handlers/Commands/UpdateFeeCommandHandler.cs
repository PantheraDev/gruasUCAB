using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Exceptions;
using OrderMs.Common.Primitives;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class UpdateInsuredVehicleCommandHandler : IRequestHandler<UpdateInsuredVehicleCommand, Guid>
    {
        private readonly IInsuredVehicleRepository _insuredVehicleRepository;
        public readonly IClientRepository _clientRepository;
        public UpdateInsuredVehicleCommandHandler(IInsuredVehicleRepository insuredVehicleRepository, IClientRepository clientRepository)
        {
            _insuredVehicleRepository = insuredVehicleRepository ?? throw new ArgumentNullException(nameof(insuredVehicleRepository)); //*Valido que estas inyecciones sean exitosas
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }

        public async Task<Guid> Handle(UpdateInsuredVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldInsuredVehicle = await _insuredVehicleRepository.GetByIdAsync(InsuredVehicleId.Create(request.Id)!);

                if (oldInsuredVehicle == null) throw new InsuredVehicleNotFoundException("InsuredVehicle not found");


                if (request.InsuredVehicle.Weight != null)
                {
                    oldInsuredVehicle = InsuredVehicle.Update(oldInsuredVehicle, InsuredVehicleWeight.Create(request.InsuredVehicle.Weight.Value), null, null, null, null, null, null);
                }
                if (request.InsuredVehicle.LicensePlate != null)
                {
                    oldInsuredVehicle = InsuredVehicle.Update(oldInsuredVehicle, null, InsuredVehicleLicensePlate.Create(request.InsuredVehicle.LicensePlate), null, null, null, null, null);
                }
                if (request.InsuredVehicle.Brand != null)
                {
                    oldInsuredVehicle = InsuredVehicle.Update(oldInsuredVehicle, null, null, InsuredVehicleBrand.Create(request.InsuredVehicle.Brand), null, null, null, null);
                }
                if (request.InsuredVehicle.Model != null)
                {
                    oldInsuredVehicle = InsuredVehicle.Update(oldInsuredVehicle, null, null, null, InsuredVehicleModel.Create(request.InsuredVehicle.Model), null, null, null);
                }
                if (request.InsuredVehicle.Year != null)
                {
                    oldInsuredVehicle = InsuredVehicle.Update(oldInsuredVehicle, null, null, null, null, InsuredVehicleYear.Create(request.InsuredVehicle.Year), null, null);
                }
                if (request.InsuredVehicle.Color != null)
                {
                    oldInsuredVehicle = InsuredVehicle.Update(oldInsuredVehicle, null, null, null, null, null, InsuredVehicleColor.Create(request.InsuredVehicle.Color), null);
                }
                if (request.InsuredVehicle.ClientId != null && await _clientRepository.ExistAsync(ClientId.Create(request.InsuredVehicle.ClientId.Value)!))
                {
                    oldInsuredVehicle = InsuredVehicle.Update(oldInsuredVehicle, null, null, null, null, null, null, ClientId.Create(request.InsuredVehicle.ClientId!.Value));
                }

                //TODO: Hay que hacer que se guarde el UpdatedBy

                await _insuredVehicleRepository.UpdateAsync(oldInsuredVehicle);

                return oldInsuredVehicle.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}