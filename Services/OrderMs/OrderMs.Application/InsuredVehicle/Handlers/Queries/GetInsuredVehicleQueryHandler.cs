using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Queries;
using OrderMs.Common.Dtos.Response;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Handlers.Queries
{
    public class GetInsuredVehicleQueryHandler : IRequestHandler<GetInsuredVehicleQuery, GetInsuredVehicleDto>
    {
        public IInsuredVehicleRepository _insuredVehicleRepository;

        public GetInsuredVehicleQueryHandler(IInsuredVehicleRepository insuredVehicleRepository)
        {
            _insuredVehicleRepository = insuredVehicleRepository;
        }

        public async Task<GetInsuredVehicleDto> Handle(GetInsuredVehicleQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("InsuredVehicle id is required");
            var insuredVehicleId = InsuredVehicleId.Create(request.Id);
            var insuredVehicle = await _insuredVehicleRepository.GetByIdAsync(insuredVehicleId!);

            if (insuredVehicle == null || insuredVehicle.IsDeleted) throw new InsuredVehicleNotFoundException("InsuredVehicle not found");

            return new GetInsuredVehicleDto(
                    insuredVehicle.Id.Value,
                    insuredVehicle.CreatedBy,
                    insuredVehicle.LicensePlate.Value,
                    insuredVehicle.Brand.Value,
                    insuredVehicle.Model.Value,
                    insuredVehicle.Color.Value,
                    insuredVehicle.Weight.Value,
                    insuredVehicle.Year.Value,
                    insuredVehicle.ClientId.Value
                );
        }
    }
}