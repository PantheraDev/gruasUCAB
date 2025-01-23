using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;
using ProviderMs.Domain.Entities;


namespace ProviderMs.Application.Command
{
    public class CreateDepartmentCommand : IRequest<Guid>
    {
        public CreateDepartmentdto Department { get; set; }

        public CreateDepartmentCommand(CreateDepartmentdto department)
        {
            Department = department;
        }
    }
}