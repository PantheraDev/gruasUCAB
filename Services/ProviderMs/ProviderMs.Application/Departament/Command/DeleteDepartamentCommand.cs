using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class DeleteDepartamentCommand : IRequest<Guid>
    {
        public Guid DepartamentId { get; set; }

        public DeleteDepartamentCommand(Guid departament)
        {
            DepartamentId = departament;
        }
    }
}