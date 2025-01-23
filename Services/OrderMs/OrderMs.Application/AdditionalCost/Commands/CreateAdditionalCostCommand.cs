using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Request;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class CreateAdditionalCostCommand : IRequest<Guid>
    {
        public CreateAdditionalCostDto AdditionalCost { get; set; }

        public CreateAdditionalCostCommand(CreateAdditionalCostDto additionalCost)
        {
            AdditionalCost = additionalCost;
        }
    }
}