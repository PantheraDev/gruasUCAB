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
    public class DeleteAdditionalCostCommandHandler : IRequestHandler<DeleteAdditionalCostCommand, Guid>
    {
        private readonly IAdditionalCostRepository _additionalCostRepository;
        public DeleteAdditionalCostCommandHandler(IAdditionalCostRepository additionalCostRepository)
        {
            _additionalCostRepository = additionalCostRepository ?? throw new ArgumentNullException(nameof(additionalCostRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(DeleteAdditionalCostCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var additionalCostId = AdditionalCostId.Create(request.AdditionalCostId);
                await _additionalCostRepository.DeleteAsync(additionalCostId!);
                return additionalCostId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}