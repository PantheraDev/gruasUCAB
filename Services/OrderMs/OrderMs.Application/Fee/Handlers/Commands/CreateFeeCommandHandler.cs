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
    public class CreateFeeCommandHandler : IRequestHandler<CreateFeeCommand, Guid>
    {
        private readonly IFeeRepository _FeeRepository;
        public CreateFeeCommandHandler(IFeeRepository FeeRepository)
        {
            _FeeRepository = FeeRepository ?? throw new ArgumentNullException(nameof(FeeRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(CreateFeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //TODO: Revisar este validador
                var validator = new CreateFeeValidator();
                await validator.ValidateRequest(request.Fee);

                //* Se crean los Value Objects
                var feeId = FeeId.Create();
                var feeBasePrice = FeeBasePrice.Create(request.Fee.BasePrice);
                var feeRadius = FeeRadius.Create(request.Fee.Radius);
                var feePriceXKm = FeePriceXKm.Create(request.Fee.PriceXKm);

                //* Se crea el Feee
                var fee = new Fee(feeId, feeBasePrice, feeRadius, feePriceXKm);

                //* Se agrega el Feee a la BD
                await _FeeRepository.AddAsync(fee);

                //* Retorna la id del Feee
                return fee.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}