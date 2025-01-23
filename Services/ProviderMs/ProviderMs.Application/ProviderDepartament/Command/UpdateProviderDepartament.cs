using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class UpdateProviderDepartmentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateProviderDepartmentDto ProviderDepartment { get; set; }

        public UpdateProviderDepartmentCommand(Guid id, UpdateProviderDepartmentDto providerDepartment)
        {
            ProviderDepartment = providerDepartment;
            Id = id;
        }
    }
}