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
    public class DeleteFeeCommandHandler : IRequestHandler<DeleteFeeCommand, Guid>
    {
        private readonly IFeeRepository _FeeRepository;
        public DeleteFeeCommandHandler(IFeeRepository FeeRepository)
        {
            _FeeRepository = FeeRepository ?? throw new ArgumentNullException(nameof(FeeRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(DeleteFeeCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var feeId = FeeId.Create(request.FeeId);
                await _FeeRepository.DeleteAsync(feeId!);
                return feeId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}