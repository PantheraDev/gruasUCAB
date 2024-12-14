using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Primitives;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class DeleteIncidentCommandHandler : IRequestHandler<DeleteIncidentCommand, Guid>
    {
        private readonly IIncidentRepository _incidentRepository;
        public DeleteIncidentCommandHandler(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository ?? throw new ArgumentNullException(nameof(incidentRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(DeleteIncidentCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var incidentId = IncidentId.Create(request.IncidentId);
                await _incidentRepository.DeleteAsync(incidentId!);
                return incidentId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}