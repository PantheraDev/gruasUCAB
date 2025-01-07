using MediatR;
using ProviderMs.Common.dto.Request;

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