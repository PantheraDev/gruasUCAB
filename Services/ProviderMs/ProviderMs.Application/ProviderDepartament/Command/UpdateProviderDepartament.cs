using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class UpdateProviderDepartamentCommand : IRequest<Guid>
    {
        public Guid DepartamentId { get; set; }
        public UpdateProviderDepartamentDto ProviderDepartament { get; set; }

        public UpdateProviderDepartamentCommand(Guid providerId, Guid departamentId, UpdateProviderDto provider)
        {
            ProviderDepartament = ProviderDepartament;
            DepartamentId = departamentId;
        }
    }
}