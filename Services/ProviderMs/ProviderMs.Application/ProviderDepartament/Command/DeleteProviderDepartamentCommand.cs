using MediatR;

namespace ProviderMs.Application.Command
{
    public class DeleteProviderDepartmentCommand : IRequest<Guid>
    {
        public Guid ProviderDepartmentId { get; set; }

        public DeleteProviderDepartmentCommand(Guid providerDepartment)
        {
            ProviderDepartmentId = providerDepartment;
        }
    }
}