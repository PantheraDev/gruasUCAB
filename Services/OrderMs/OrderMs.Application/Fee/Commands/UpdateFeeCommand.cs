using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Request;

namespace OrderMs.Application.Commands
{
    public class UpdateFeeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateFeeDto Fee { get; set; }

        public UpdateFeeCommand(Guid id, UpdateFeeDto fee)
        {
            Fee = fee;
            Id = id;
        }
    }
}