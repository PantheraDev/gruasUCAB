using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Primitives;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class CreateIncidentCommandHandler : IRequestHandler<CreateIncidentCommand, Guid>
    {
        private readonly IIncidentRepository _incidentRepository;
        public CreateIncidentCommandHandler(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository ?? throw new ArgumentNullException(nameof(incidentRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //TODO: Revisar este validador
                var validator = new CreateIncidentValidator();
                await validator.ValidateRequest(request.Incident);

                //* Se crean los Value Objects
                var incidentId = IncidentId.Create();
                var incidentDescription = IncidentDescription.Create(request.Incident.Description);
                var incidentDestinyLocation = IncidentDestinyLocation.Create(request.Incident.DestinyLocation);
                var incidentDate = IncidentDate.Create(request.Incident.Date.ToUniversalTime());

                //* Se crea el Incidente
                var incident = new Incident(incidentId, incidentDescription, incidentDestinyLocation, incidentDate);

                //* Se agrega el Incidente a la BD
                await _incidentRepository.AddAsync(incident);

                //* Retorna la id del Incidente
                return incident.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}