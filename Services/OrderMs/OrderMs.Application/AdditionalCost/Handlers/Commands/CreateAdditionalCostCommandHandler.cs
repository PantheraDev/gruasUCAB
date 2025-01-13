using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Primitives;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class CreateAdditionalCostCommandHandler : IRequestHandler<CreateAdditionalCostCommand, Guid>
    {
        private readonly IAdditionalCostRepository _additionalCostRepository;
        public CreateAdditionalCostCommandHandler(IAdditionalCostRepository additionalCostRepository)
        {
            _additionalCostRepository = additionalCostRepository ?? throw new ArgumentNullException(nameof(additionalCostRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(CreateAdditionalCostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //TODO: Revisar este validador
                var validator = new CreateAdditionalCostValidator();
                await validator.ValidateRequest(request.AdditionalCost);

                //* Se crean los Value Objects
                var additionalCostId = AdditionalCostId.Create();
                var additionalCostDescription = AdditionalCostDescription.Create(request.AdditionalCost.Description);
                var additionalCostValue = AdditionalCostValue.Create(request.AdditionalCost.Value);
                var orderId = OrderId.Create(request.AdditionalCost.OrderId);
                
                //* Se crea el AdditionalCoste
                var additionalCost = new AdditionalCost(additionalCostId, additionalCostDescription, additionalCostValue, orderId!);

                //* Se agrega el AdditionalCoste a la BD
                await _additionalCostRepository.AddAsync(additionalCost);

                //* Retorna la id del AdditionalCoste
                return additionalCost.Id.Value;
            }
            catch
            {
                throw;
            }
        }
    }
}