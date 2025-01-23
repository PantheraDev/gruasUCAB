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
    public class UpdateFeeCommandHandler : IRequestHandler<UpdateFeeCommand, Guid>
    {
        private readonly IFeeRepository _FeeRepository;
        public UpdateFeeCommandHandler(IFeeRepository FeeRepository)
        {
            _FeeRepository = FeeRepository ?? throw new ArgumentNullException(nameof(FeeRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(UpdateFeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldFee = await _FeeRepository.GetByIdAsync(FeeId.Create(request.Id)!);

                if (oldFee == null) throw new FeeNotFoundException("Fee not found");


                if (request.Fee.BasePrice != null)
                {
                    oldFee = Fee.Update(oldFee, FeeBasePrice.Create(request.Fee.BasePrice.Value), null, null);
                }
                if (request.Fee.Radius != null)
                {
                    oldFee = Fee.Update(oldFee, null, FeeRadius.Create(request.Fee.Radius.Value), null);
                }
                if (request.Fee.PriceXKm != null)
                {
                    oldFee = Fee.Update(oldFee, null, null, FeePriceXKm.Create(request.Fee.PriceXKm.Value));
                }

                //TODO: Hay que hacer que se guarde el UpdatedBy

                await _FeeRepository.UpdateAsync(oldFee);

                return oldFee.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}