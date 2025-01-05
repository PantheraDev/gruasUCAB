using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Domain.ValueObjects;
using ProviderMs.Application.Validators;
using ProviderMs.Common.Primitives;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;
using ProviderMs.Common.Enums;

namespace ProviderMs.Application.Command
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class CreateTowCommandHandler : IRequestHandler<CreateTowCommand, Guid>
    {
        private readonly ITowRepository _vehicleRepository;
        public CreateTowCommandHandler(ITowRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(CreateTowCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //TODO: Revisar este validador
                var validator = new CreateTowValidator();
                await validator.ValidateRequest(request.Vehicle);

                //* Se crean los Value Objects
                var towId= VehicleId.Create();
                var towcolor = VehicleColor.Create(request.Vehicle.Color);
                var towyear = VehicleYear.Create(request.Vehicle.Year);
                var towmodel = VehicleModel.Create(request.Vehicle.Model);
                var towbrand = VehicleBrand.Create(request.Vehicle.Brand);
                var towlicenseplate = VehicleLicensePlate.Create(request.Vehicle.LicensePlate);
                var towlocation = TowLocation.Create(request.Vehicle.TowLocation);
                var towavailability = (TowAvailability)Enum.Parse(typeof(TowAvailability), request.Vehicle.TowAvailability);
                var towtype = (TowType)Enum.Parse(typeof(TowType), request.Vehicle.TowType);
                var towdriver = TowDriver.Create(request.Vehicle.TowDriver);
                //* Se crea el cliente
                var providerId = ProviderId.Create(request.Vehicle.ProviderId);
                var vehicle = new Tow(towId, towcolor, towyear, towmodel, towbrand, towlicenseplate, towlocation, towavailability, towtype, providerId, towdriver);

                //* Se agrega el cliente a la BD
                await _vehicleRepository.AddAsync(vehicle);

                //* Retorna la id del cliente
                return vehicle.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}