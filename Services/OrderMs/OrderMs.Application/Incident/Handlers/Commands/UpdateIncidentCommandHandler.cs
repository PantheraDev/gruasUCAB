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
    public class UpdateIncidentCommandHandler : IRequestHandler<UpdateIncidentCommand, Guid>
    {
        private readonly IIncidentRepository _incidentRepository;
        public UpdateIncidentCommandHandler(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository ?? throw new ArgumentNullException(nameof(incidentRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(UpdateIncidentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldIncident = await _incidentRepository.GetByIdAsync(IncidentId.Create(request.Id)!);

                if (oldIncident == null) throw new IncidentNotFoundException("Incident not found");


                if (request.Incident.Description != null)
                {
                    oldIncident = Incident.Update(oldIncident, IncidentDescription.Create(request.Incident.Description), null, null);
                }
                if (request.Incident.DestinyLocation != null)
                {
                    oldIncident = Incident.Update(oldIncident, null, IncidentDestinyLocation.Create(request.Incident.DestinyLocation), null);
                }
                if (request.Incident.Date != null)
                {
                    oldIncident = Incident.Update(oldIncident, null, null, IncidentDate.Create(request.Incident.Date.Value.ToUniversalTime()));
                }

                //TODO: Hay que hacer que se guarde el UpdatedBy

                await _incidentRepository.UpdateAsync(oldIncident);

                return oldIncident.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}