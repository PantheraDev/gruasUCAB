using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Primitives;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class DeletePolicyCommandHandler : IRequestHandler<DeletePolicyCommand, Guid>
    {
        private readonly IPolicyRepository _policyRepository;
        public DeletePolicyCommandHandler(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository ?? throw new ArgumentNullException(nameof(policyRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var policyId = PolicyId.Create(request.PolicyId);
                await _policyRepository.DeleteAsync(policyId!);
                return policyId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}