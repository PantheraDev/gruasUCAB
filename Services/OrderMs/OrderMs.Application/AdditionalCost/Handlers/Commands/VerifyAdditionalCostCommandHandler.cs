using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Enums;
using OrderMs.Common.Exceptions;
using OrderMs.Common.Primitives;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class VerifyAdditionalCostCommandHandler : IRequestHandler<VerifyAdditionalCostCommand, AdditionalCostVerified>
    {
        private readonly IAdditionalCostRepository _additionalCostRepository;
        public VerifyAdditionalCostCommandHandler(IAdditionalCostRepository additionalCostRepository)
        {
            _additionalCostRepository = additionalCostRepository ?? throw new ArgumentNullException(nameof(additionalCostRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<AdditionalCostVerified> Handle(VerifyAdditionalCostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldAdditionalCost = await _additionalCostRepository.GetByIdAsync(AdditionalCostId.Create(request.Id)!);

                if (oldAdditionalCost == null) throw new AdditionalCostNotFoundException("AdditionalCost not found");

                if (oldAdditionalCost.Verified == AdditionalCostVerified.Verificado)
                {
                    throw new InvalidAttributeException("AdditionalCost already verified");
                }
                oldAdditionalCost = AdditionalCost.Update(oldAdditionalCost, null, null, AdditionalCostVerified.Verificado);

                await _additionalCostRepository.UpdateAsync(oldAdditionalCost);

                return oldAdditionalCost.Verified;
            }
            catch
            {
                throw;
            }



        }
    }
}