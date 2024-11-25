using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Request;

namespace OrderMs.Application.Commands
{
    public class UpdateIncidentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateIncidentDto Incident { get; set; }

        public UpdateIncidentCommand(Guid id, UpdateIncidentDto incident)
        {
            Incident = incident;
            Id = id;
        }
    }
}