using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;
using ProviderMs.Domain.Entities;


namespace ProviderMs.Application.Command
{
    public class CreateProviderCommand : IRequest<Guid>
    {
        public CreateProviderdto Provider { get; set; }
        
        public CreateProviderCommand(CreateProviderdto provider)
        {
            Provider = provider;
        }
    }
}