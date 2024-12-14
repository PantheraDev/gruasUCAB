using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Request;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class CreateOrderCommand : IRequest<Guid>
    {
        public CreateOrderDto Order { get; set; }

        public CreateOrderCommand(CreateOrderDto order)
        {
            Order = order;
        }
    }
}