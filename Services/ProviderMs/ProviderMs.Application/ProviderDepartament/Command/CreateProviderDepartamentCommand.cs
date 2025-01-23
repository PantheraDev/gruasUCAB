using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class CreateProviderDepartmentCommand : IRequest<Guid>
    {
        public CreateProviderDepartmentdto ProviderDepartment { get; set; }

        public CreateProviderDepartmentCommand(CreateProviderDepartmentdto providerDepartment)
        {
            ProviderDepartment = providerDepartment;
        }
    }
}