using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace OrderMs.Application.Tow.Commands
{
    public class AcceptOrderCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }
        public AcceptOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}