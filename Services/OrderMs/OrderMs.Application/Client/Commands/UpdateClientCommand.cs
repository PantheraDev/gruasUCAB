using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Request;

namespace OrderMs.Application.Commands
{
    public class UpdateClientCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateClientDto Client { get; set; }

        public UpdateClientCommand(Guid id, UpdateClientDto client)
        {
            Client = client;
            Id = id;
        }
    }
}