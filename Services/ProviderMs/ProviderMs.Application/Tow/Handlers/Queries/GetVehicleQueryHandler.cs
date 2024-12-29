using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Application.Queries;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetTowQueryHandler : IRequestHandler<GetTowQuery, GetTow>
    {
        public ITowRepository _TowRepository;

        public GetTowQueryHandler(ITowRepository TowRepository)
        {
            _TowRepository = TowRepository;
        }

        public async Task<GetTow> Handle(GetTowQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("Tow id is required");
            var vehicleId = VehicleId.Create(request.Id);
            var tow = await _TowRepository.GetByIdAsync(vehicleId!);

            if (tow is null || tow.IsDeleted) throw new VehicleNotFoundException("Vehicle not found");

            return new GetTow(
                tow.Id.Value,
                tow.Color.Value,
                tow.Year.Value,
                tow.Model.Value,
                tow.Brand.Value,
                tow.LicensePlate.Value,
                tow.TowLocation.Value,
                tow.TowAvailability.Value,
                tow.TowType,
                tow.ProviderId.Value,
                tow.TowDriver.Value,
                tow.CreatedBy
            );
        }
    }
}