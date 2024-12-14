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
    public class GetAllIncidentsQueryHandler : IRequestHandler<GetAllIncidentsQuery, List<GetIncidentDto>>
    {
        public IIncidentRepository _incidentRepository;

        public GetAllIncidentsQueryHandler(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public async Task<List<GetIncidentDto>> Handle(GetAllIncidentsQuery request, CancellationToken cancellationToken)
        {
            var incident = await _incidentRepository.GetAllAsync();

            if (incident == null) throw new IncidentNotFoundException("Incidents are empty");

            return incident.Where(c => !c.IsDeleted).Select(c =>
                new GetIncidentDto(
                    c.Id.Value,
                    c.CreatedBy,
                    c.Description.Value,
                    c.DestinyLocation.Value,
                    c.Date.Value
                )
            ).ToList();
        }
    }
}