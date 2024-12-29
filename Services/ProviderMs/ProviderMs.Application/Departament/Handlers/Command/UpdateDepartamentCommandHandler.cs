using MediatR;
using ProviderMs.Application.Validators;
using ProviderMs.Common.Exceptions;
using ProviderMs.Common.Primitives;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class UpdateDepartamentCommandHandler : IRequestHandler<UpdateDepartamentCommand, Guid>
    {
        private readonly IDepartamentRepository _DepartamentRepository;
        public UpdateDepartamentCommandHandler(IDepartamentRepository DepartamentRepository)
        {
            _DepartamentRepository = DepartamentRepository ?? throw new ArgumentNullException(nameof(DepartamentRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(UpdateDepartamentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldDepartament = await _DepartamentRepository.GetByIdAsync(DepartamentId.Create(request.Id)!);

                if (oldDepartament == null) throw new DepartamentNotFoundException("Departament not found");


                if (request.Departament.Name != null)
                {
                    oldDepartament = Departament.Update(oldDepartament, DepartamentName.Create(request.Departament.Name));
                }
                

                //TODO: Hay que hacer que se guarde el UpdatedBy

                await _DepartamentRepository.UpdateAsync(oldDepartament);

                return oldDepartament.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}