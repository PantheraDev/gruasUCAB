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
    public class UpdatePolicyCommandHandler : IRequestHandler<UpdatePolicyCommand, Guid>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IInsuredVehicleRepository _insuredVehicleRepository;
        private readonly IFeeRepository _feeRepository;
        public UpdatePolicyCommandHandler(IPolicyRepository policyRepository, IInsuredVehicleRepository insuredVehicleRepository, IFeeRepository feeRepository)
        {
            _policyRepository = policyRepository ?? throw new ArgumentNullException(nameof(policyRepository)); //*Valido que estas inyecciones sean exitosas
            _insuredVehicleRepository = insuredVehicleRepository ?? throw new ArgumentNullException(nameof(insuredVehicleRepository));
            _feeRepository = feeRepository ?? throw new ArgumentNullException(nameof(feeRepository));
        }

        public async Task<Guid> Handle(UpdatePolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldPolicy = await _policyRepository.GetByIdAsync(PolicyId.Create(request.Id)!);

                if (oldPolicy == null) throw new PolicyNotFoundException("Policy not found");


                if (request.Policy.Coverage != null)
                {
                    oldPolicy = Policy.Update(oldPolicy, PolicyCoverage.Create(request.Policy.Coverage.Value), null, null, null, null);
                }
                if (request.Policy.ExpirationDate != null)
                {
                    oldPolicy = Policy.Update(oldPolicy, null, null, PolicyExpirationDate.Create(request.Policy.ExpirationDate.Value.ToUniversalTime()), null, null);
                }
                if (request.Policy.IssueDate != null)
                {
                    oldPolicy = Policy.Update(oldPolicy, null, PolicyIssueDate.Create(request.Policy.IssueDate.Value.ToUniversalTime()), null, null, null);
                }
                if (request.Policy.InsuredVehicleId != null && await _insuredVehicleRepository.ExistsAsync(InsuredVehicleId.Create(request.Policy.InsuredVehicleId.Value)!))
                {
                    oldPolicy = Policy.Update(oldPolicy, null, null, null, InsuredVehicleId.Create(request.Policy.InsuredVehicleId.Value), null);
                }
                if (request.Policy.FeeId != null && await _feeRepository.ExistsAsync(FeeId.Create(request.Policy.FeeId.Value)!))
                {
                    oldPolicy = Policy.Update(oldPolicy, null, null, null, null, FeeId.Create(request.Policy.FeeId.Value));
                }
                //TODO: Hay que hacer que se guarde el UpdatedBy

                await _policyRepository.UpdateAsync(oldPolicy);

                return oldPolicy.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}