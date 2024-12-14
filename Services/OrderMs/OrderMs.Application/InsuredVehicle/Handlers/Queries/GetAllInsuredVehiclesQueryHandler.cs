using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Queries;
using OrderMs.ApplicationQueries;
using OrderMs.Common.Dtos.Request;
using OrderMs.Common.Dtos.Response;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Handlers.Queries
{
    public class GetAllInsuredVehiclesQueryHandler : IRequestHandler<GetAllInsuredVehiclesQuery, List<GetInsuredVehicleDto>>
    {
        public IInsuredVehicleRepository _insuredVehicleRepository;

        public GetAllInsuredVehiclesQueryHandler(IInsuredVehicleRepository insuredVehicleRepository)
        {
            _insuredVehicleRepository = insuredVehicleRepository;
        }

        public async Task<List<GetInsuredVehicleDto>> Handle(GetAllInsuredVehiclesQuery request, CancellationToken cancellationToken)
        {
            var insuredVehicle = await _insuredVehicleRepository.GetAllAsync();

            if (insuredVehicle == null) throw new InsuredVehicleNotFoundException("InsuredVehicles are empty");

            return insuredVehicle.Where(c => !c.IsDeleted).Select(c =>
                new GetInsuredVehicleDto(
                    c.Id.Value,
                    c.CreatedBy,
                    c.LicensePlate.Value,
                    c.Brand.Value,
                    c.Model.Value,
                    c.Color.Value,
                    c.Weight.Value,
                    c.Year.Value,
                    c.ClientId.Value
                )
            ).ToList();
        }
    }
}