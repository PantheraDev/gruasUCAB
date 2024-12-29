using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MediatR;
using Microsoft.VisualBasic;
using ProviderMs.Application.Command;
using ProviderMs.Application.Validators;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    public class CreateDepartamentCommandHandler : IRequestHandler<CreateDepartamentCommand, Guid>
    {
        private readonly IDepartamentRepository _departamentRepository;

        public CreateDepartamentCommandHandler(IDepartamentRepository departamentRepository)
        {
            _departamentRepository = departamentRepository;
        }
       public async Task<Guid> Handle(CreateDepartamentCommand request, CancellationToken cancellationToken)
        {
            try{
            var validator = new CreateDepartamentValidator();
            await validator.ValidateRequest(request.Departament);
            var departamentId = DepartamentId.Create(Guid.NewGuid());
            var departamentName = DepartamentName.Create(request.Departament.Name);
            var departament = new Departament(departamentId, departamentName);
            await _departamentRepository.AddAsync(departament);
            return departament.Id.Value;

            }
            catch(Exception ex){
                throw;
            }
        }
        
    }
}