using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CreateInsuredVehicleCommandHandler : IRequestHandler<CreateInsuredVehicleCommand, Guid>
    {
        private readonly IInsuredVehicleRepository _insuredVehicleRepository;
        public readonly IClientRepository _clientRepository;
        public CreateInsuredVehicleCommandHandler(IInsuredVehicleRepository insuredVehicleRepository, IClientRepository clientRepository)
        {
            _insuredVehicleRepository = insuredVehicleRepository ?? throw new ArgumentNullException(nameof(insuredVehicleRepository)); //*Valido que estas inyecciones sean exitosas
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }

        public async Task<Guid> Handle(CreateInsuredVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //TODO: Revisar este validador
                var validator = new CreateInsuredVehicleValidator();
                await validator.ValidateRequest(request.InsuredVehicle);

                var clientId = ClientId.Create(request.InsuredVehicle.ClientId);
                if (!await _clientRepository.ExistAsync(clientId!))
                {
                    throw new ClientNotFoundException("Client not found");
                }

                //* Se crean los Value Objects
                var insuredVehicleId = InsuredVehicleId.Create();
                var insuredVehicleWeight = InsuredVehicleWeight.Create(request.InsuredVehicle.Weight);
                var insuredVehicleLicensePlate = InsuredVehicleLicensePlate.Create(request.InsuredVehicle.LicensePlate);
                var insuredVehicleBrand = InsuredVehicleBrand.Create(request.InsuredVehicle.Brand);
                var insuredVehicleModel = InsuredVehicleModel.Create(request.InsuredVehicle.Model);
                var insuredVehicleYear = InsuredVehicleYear.Create(request.InsuredVehicle.Year);
                var insuredVehicleColor = InsuredVehicleColor.Create(request.InsuredVehicle.Color);
                

                //* Se crea el InsuredVehiclee
                var insuredVehicle = new InsuredVehicle(insuredVehicleId, insuredVehicleWeight, insuredVehicleLicensePlate, insuredVehicleBrand, insuredVehicleModel, insuredVehicleYear, insuredVehicleColor, clientId!);

                //* Se agrega el InsuredVehiclee a la BD
                await _insuredVehicleRepository.AddAsync(insuredVehicle);

                //* Retorna la id del InsuredVehiclee
                return insuredVehicle.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}