using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.ApplicationQueries;
using ProviderMs.Common.dto.Request;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetAllTowsQueryHandler : IRequestHandler<GetAllTowsQuery, List<GetTow>>
    {
        public ITowRepository _vehicleRepository;

        public GetAllTowsQueryHandler(ITowRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<List<GetTow>> Handle(GetAllTowsQuery request, CancellationToken cancellationToken)
        {
            var tow = await _vehicleRepository.GetAllAsync();

            if (tow == null) throw new VehicleNotFoundException("Tow are empty");

            return tow.Where(p => !p.IsDeleted).Select(p =>
                new GetTow(
                    p.Id.Value,
                    p.Color.Value,
                    p.Year.Value,
                    p.Model.Value,
                    p.Brand.Value,
                    p.LicensePlate.Value,
                    p.TowLocation.Value,
                    p.TowAvailability.Value,
                    p.TowType,
                    p.ProviderId.Value,
                    p.TowDriver.Value,
                    p.CreatedBy
                )
            ).ToList();
        }
    }
}