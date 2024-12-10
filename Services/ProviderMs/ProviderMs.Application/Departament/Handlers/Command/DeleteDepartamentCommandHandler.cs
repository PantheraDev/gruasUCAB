using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Application.Validators;
using ProviderMs.Common.Primitives;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class DeleteDepartamentCommandHandler : IRequestHandler<DeleteDepartamentCommand, Guid>
    {
        private readonly IDepartamentRepository _DepartamentRepository;
        public DeleteDepartamentCommandHandler(IDepartamentRepository DepartamentRepository)
        {
            _DepartamentRepository = DepartamentRepository ?? throw new ArgumentNullException(nameof(DepartamentRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(DeleteDepartamentCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var departamentId = DepartamentId.Create(request.DepartamentId);
                await _DepartamentRepository.DeleteAsync(departamentId!);
                return departamentId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}