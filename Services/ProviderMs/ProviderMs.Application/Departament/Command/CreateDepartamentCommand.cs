using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;
using ProviderMs.Domain.Entities;


namespace ProviderMs.Application.Command
{
    public class CreateDepartamentCommand : IRequest<Guid>
    {
        public CreateDepartamentdto Departament { get; set; }
        
        public CreateDepartamentCommand(CreateDepartamentdto departament)
        {
            Departament = departament;
        }
    }
}