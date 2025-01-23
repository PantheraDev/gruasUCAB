using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Request;

namespace OrderMs.Application.Commands
{
    public class UpdateInsuredVehicleCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateInsuredVehicleDto InsuredVehicle { get; set; }

        public UpdateInsuredVehicleCommand(Guid id, UpdateInsuredVehicleDto insuredVehicle)
        {
            InsuredVehicle = insuredVehicle;
            Id = id;
        }
    }
}