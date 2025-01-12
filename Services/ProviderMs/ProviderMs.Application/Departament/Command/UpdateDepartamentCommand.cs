using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class UpdateDepartmentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateDepartmentDto Department { get; set; }

        public UpdateDepartmentCommand(Guid id, UpdateDepartmentDto department)
        {
            Department = department;
            Id = id;
        }
    }
}