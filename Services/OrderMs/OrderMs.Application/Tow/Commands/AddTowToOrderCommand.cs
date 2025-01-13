using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace OrderMs.Application.Tow.Commands
{
    public class AddTowToOrderCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid TowId { get; set; }
        public AddTowToOrderCommand(Guid orderId, Guid towId)
        {
            OrderId = orderId;
            TowId = towId;
        }
    }
}