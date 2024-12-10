using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class UpdateDepartamentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateDepartamentDto Departament { get; set; }

        public UpdateDepartamentCommand(Guid id, UpdateDepartamentDto departament)
        {
            Departament = departament;
            Id = id;
        }
    }
}