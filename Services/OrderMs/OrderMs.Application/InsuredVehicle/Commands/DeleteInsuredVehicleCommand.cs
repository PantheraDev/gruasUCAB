using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Request;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class DeleteInsuredVehicleCommand : IRequest<Guid>
    {
        public Guid InsuredVehicleId { get; set; }

        public DeleteInsuredVehicleCommand(Guid insuredVehicle)
        {
            InsuredVehicleId = insuredVehicle;
        }
    }
}