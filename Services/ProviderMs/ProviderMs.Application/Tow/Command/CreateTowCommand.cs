using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;
using ProviderMs.Domain.Entities;


namespace ProviderMs.Application.Command
{
    public class CreateTowCommand : IRequest<Guid>
    {
        public CreateTowdto Vehicle { get; set; }
        
        public CreateTowCommand(CreateTowdto vehicle)
        {
            Vehicle = vehicle;
        }
    }
}