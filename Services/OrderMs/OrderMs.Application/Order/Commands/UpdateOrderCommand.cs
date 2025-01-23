using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Request;

namespace OrderMs.Application.Commands
{
    public class UpdateOrderCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateOrderDto Order { get; set; }

        public UpdateOrderCommand(Guid id, UpdateOrderDto order)
        {
            Order = order;
            Id = id;
        }
    }
}