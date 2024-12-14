using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Request;

namespace OrderMs.Application.Commands
{
    public class UpdatePolicyCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdatePolicyDto Policy { get; set; }

        public UpdatePolicyCommand(Guid id, UpdatePolicyDto policy)
        {
            Policy = policy;
            Id = id;
        }
    }
}