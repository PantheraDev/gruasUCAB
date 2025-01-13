using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class ChangeAvailabilityCommand : IRequest<string>
    {
        public Guid TowId { get; set; }

        public ChangeAvailabilityCommand(Guid towId)
        {
            TowId = towId;
        }
    }
}