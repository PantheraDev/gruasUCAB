using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Request;

namespace OrderMs.Application.Commands
{
    public class UpdateAdditionalCostCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateAdditionalCostDto AdditionalCost { get; set; }

        public UpdateAdditionalCostCommand(Guid id, UpdateAdditionalCostDto additionalCost)
        {
            AdditionalCost = additionalCost;
            Id = id;
        }
    }
}