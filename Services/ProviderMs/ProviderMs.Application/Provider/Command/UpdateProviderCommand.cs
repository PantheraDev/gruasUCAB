using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class UpdateProviderCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateProviderDto Provider { get; set; }

        public UpdateProviderCommand(Guid id, UpdateProviderDto provider)
        {
            Provider = provider;
            Id = id;
        }
    }
}