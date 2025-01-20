using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class AddDriverCommand : IRequest<Guid>
    {
        public Guid TowId { get; set; }
        public Guid DriverId { get; set; }

        public AddDriverCommand(Guid towId, Guid driverId)
        {
            TowId = towId;
            DriverId = driverId;
        }
    }
}