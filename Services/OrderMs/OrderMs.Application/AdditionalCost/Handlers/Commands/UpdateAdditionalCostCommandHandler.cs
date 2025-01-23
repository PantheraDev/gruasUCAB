using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Exceptions;
using OrderMs.Common.Primitives;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class UpdateAdditionalCostCommandHandler : IRequestHandler<UpdateAdditionalCostCommand, Guid>
    {
        private readonly IAdditionalCostRepository _additionalCostRepository;
        public UpdateAdditionalCostCommandHandler(IAdditionalCostRepository additionalCostRepository)
        {
            _additionalCostRepository = additionalCostRepository ?? throw new ArgumentNullException(nameof(additionalCostRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(UpdateAdditionalCostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldAdditionalCost = await _additionalCostRepository.GetByIdAsync(AdditionalCostId.Create(request.Id)!);

                if (oldAdditionalCost == null) throw new AdditionalCostNotFoundException("AdditionalCost not found");


                if (request.AdditionalCost.Description != null)
                {
                    oldAdditionalCost = AdditionalCost.Update(oldAdditionalCost, AdditionalCostDescription.Create(request.AdditionalCost.Description), null, null);
                }
                if (request.AdditionalCost.Value != null)
                {
                    oldAdditionalCost = AdditionalCost.Update(oldAdditionalCost, null, AdditionalCostValue.Create(request.AdditionalCost.Value.Value), null);
                }

                //TODO: Hay que hacer que se guarde el UpdatedBy

                await _additionalCostRepository.UpdateAsync(oldAdditionalCost);

                return oldAdditionalCost.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}