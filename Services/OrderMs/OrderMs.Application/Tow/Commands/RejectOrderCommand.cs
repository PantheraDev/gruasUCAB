using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace OrderMs.Application.Tow.Commands
{
    public class RejectOrderCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }
        public RejectOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}