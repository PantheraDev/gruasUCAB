using MediatR;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Command
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class DeleteProviderDepartmentCommandHandler : IRequestHandler<DeleteProviderDepartmentCommand, Guid>
    {
        private readonly IProviderDepartmentRepository _providerDepartmentRepository;
        public DeleteProviderDepartmentCommandHandler(IProviderDepartmentRepository providerDepartmentRepository)
        {
            _providerDepartmentRepository = providerDepartmentRepository ?? throw new ArgumentNullException(nameof(providerDepartmentRepository)); //*Valido que estas inyecciones sean exitosas  
        }

        public async Task<Guid> Handle(DeleteProviderDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var providerDepartmentId = ProviderDepartmentId.Create(request.ProviderDepartmentId);
                await _providerDepartmentRepository.DeleteAsync(providerDepartmentId!);
                return providerDepartmentId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}