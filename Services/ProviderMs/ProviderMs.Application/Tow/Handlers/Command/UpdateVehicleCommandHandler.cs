using MediatR;
using ProviderMs.Domain.ValueObjects;
using ProviderMs.Application.Validators;
using ProviderMs.Common.Exceptions;
using ProviderMs.Common.Primitives;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using ProviderMs.Common.Enums;


namespace ProviderMs.Application.Command
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class UpdateTowCommandHandler : IRequestHandler<UpdateTowCommand, Guid>
    {
        private readonly ITowRepository _VehicleRepository;
        public UpdateTowCommandHandler(ITowRepository VehicleRepository)
        {
            _VehicleRepository = VehicleRepository ?? throw new ArgumentNullException(nameof(VehicleRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(UpdateTowCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldTow = await _VehicleRepository.GetByIdAsync(VehicleId.Create(request.Id)!);

                if (oldTow == null) throw new VehicleNotFoundException("Tow not found");


                if (request.Tow.Color != null)
                {
                    oldTow = Tow.Update(oldTow, VehicleColor.Create(request.Tow.Color), null, null, null, null, null, null, null, null, null);
                }
                if (request.Tow.Year != null)
                {
                    oldTow = Tow.Update(oldTow, null, VehicleYear.Create(request.Tow.Year), null, null, null, null, null, null, null, null);
                }
                if (request.Tow.Model != null)
                {
                    oldTow = Tow.Update(oldTow, null, null, VehicleModel.Create(request.Tow.Model), null, null, null, null, null, null, null);
                }
                if (request.Tow.Brand != null)
                {
                    oldTow = Tow.Update(oldTow, null, null, null, VehicleBrand.Create(request.Tow.Brand), null,null ,null ,null, null, null);
                }
                if (request.Tow.LicensePlate != null)
                {
                    oldTow = Tow.Update(oldTow, null, null, null, null, VehicleLicensePlate.Create(request.Tow.LicensePlate), null, null, null, null, null);
                }
                if(request.Tow.TowLocation !=null)
                {
                    oldTow = Tow.Update(oldTow, null, null,null,null,null,TowLocation.Create(request.Tow.TowLocation),null,null, null, null);
                }
                if(request.Tow.TowAvailability != null)
                {
                    oldTow = Tow.Update(oldTow, null,null,null,null,null,null,(TowAvailability)Enum.Parse(typeof(TowAvailability), request.Tow.TowAvailability), null, null, null);
                }
                if(request.Tow.TowType != null)
                {
                    oldTow = Tow.Update(oldTow, null,null,null,null,null,null,null,(TowType)Enum.Parse(typeof(TowType), request.Tow.TowType), null, null);
                }
                if(request.Tow.ProviderId != null)
                {
                    oldTow = Tow.Update(oldTow, null,null,null,null,null,null,null,null,ProviderId.Create(request.Tow.ProviderId),null);
                }
                if(request.Tow.TowDriver != null)
                {
                    oldTow = Tow.Update(oldTow, null,null,null,null,null,null,null,null,null,TowDriver.Create(request.Tow.TowDriver));
                }

                //TODO: Hay que hacer que se guarde el UpdatedBy

                await _VehicleRepository.UpdateAsync(oldTow);

                return oldTow.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}