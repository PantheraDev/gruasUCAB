using MediatR;

namespace ProviderMs.Application.Command
{
    public class DeleteProviderDepartamentCommand : IRequest<Guid>
    {
        public Guid ProviderId { get; set; }
        public Guid DepartamentId { get; set; }

        public DeleteProviderDepartamentCommand(Guid provider, Guid departament)
        {
            ProviderId = provider;
            DepartamentId = departament;
        }
    }
}