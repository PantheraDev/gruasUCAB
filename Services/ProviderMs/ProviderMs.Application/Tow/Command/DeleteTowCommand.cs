using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class DeleteTowCommand : IRequest<Guid>
    {
        public Guid VehicleId { get; set; }

        public DeleteTowCommand(Guid tow)
        {
            VehicleId = tow;
        }
    }
}