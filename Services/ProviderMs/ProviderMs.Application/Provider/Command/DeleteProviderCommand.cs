using MediatR;

namespace ProviderMs.Application.Command
{
    public class DeleteProviderCommand : IRequest<Guid>
    {
        public Guid ProviderId { get; set; }

        public DeleteProviderCommand(Guid provider)
        {
            ProviderId = provider;
        }
    }
}