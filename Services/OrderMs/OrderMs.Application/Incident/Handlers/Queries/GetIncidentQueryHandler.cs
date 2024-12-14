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
    public class GetIncidentQueryHandler : IRequestHandler<GetIncidentQuery, GetIncidentDto>
    {
        public IIncidentRepository _incidentRepository;

        public GetIncidentQueryHandler(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }
        public async Task<GetIncidentDto> Handle(GetIncidentQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("Incident id is required");
            var incidentId = IncidentId.Create(request.Id);
            var incident = await _incidentRepository.GetByIdAsync(incidentId!);

            if (incident == null || incident.IsDeleted) throw new IncidentNotFoundException("Incident not found");

            return new GetIncidentDto(
                    incident.Id.Value,
                    incident.CreatedBy,
                    incident.Description.Value,
                    incident.DestinyLocation.Value,
                    incident.Date.Value
                );
        }
    }
}