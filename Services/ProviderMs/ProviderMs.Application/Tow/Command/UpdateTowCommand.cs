using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class UpdateTowCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateTowDto Tow { get; set; }

        public UpdateTowCommand(Guid id, UpdateTowDto tow)
        {
            Tow = tow;
            Id = id;
        }
    }
}