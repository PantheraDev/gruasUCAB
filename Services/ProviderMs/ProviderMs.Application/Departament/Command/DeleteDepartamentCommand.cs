using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class DeleteDepartmentCommand : IRequest<Guid>
    {
        public Guid DepartmentId { get; set; }

        public DeleteDepartmentCommand(Guid department)
        {
            DepartmentId = department;
        }
    }
}