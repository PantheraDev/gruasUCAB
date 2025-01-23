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
    public class CreatePolicyCommandHandler : IRequestHandler<CreatePolicyCommand, Guid>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IInsuredVehicleRepository _insuredVehicleRepository;
        private readonly IFeeRepository _feeRepository;
        public CreatePolicyCommandHandler(IPolicyRepository policyRepository, IInsuredVehicleRepository insuredVehicleRepository, IFeeRepository feeRepository)
        {
            _policyRepository = policyRepository ?? throw new ArgumentNullException(nameof(policyRepository)); //*Valido que estas inyecciones sean exitosas
            _insuredVehicleRepository = insuredVehicleRepository ?? throw new ArgumentNullException(nameof(insuredVehicleRepository));
            _feeRepository = feeRepository ?? throw new ArgumentNullException(nameof(feeRepository));
        }

        public async Task<Guid> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //TODO: Revisar este validador
                var validator = new CreatePolicyValidator();
                await validator.ValidateRequest(request.Policy);

                var insuredVehicleId = InsuredVehicleId.Create(request.Policy.InsuredVehicleId);
                if (!await _insuredVehicleRepository.ExistsAsync(insuredVehicleId!))
                {
                    throw new Exception("InsuredVehicleId not found");
                }

                var feeId = FeeId.Create(request.Policy.FeeId);
                if (!await _feeRepository.ExistsAsync(feeId!))
                {
                    throw new Exception("FeeId not found");
                }

                //* Se crean los Value Objects
                var policyId = PolicyId.Create();
                var policyCoverage = PolicyCoverage.Create(request.Policy.Coverage);
                var policyExpirationDate = PolicyExpirationDate.Create(request.Policy.ExpirationDate.ToUniversalTime());
                var policyIssueDate = PolicyIssueDate.Create(request.Policy.IssueDate.ToUniversalTime());

                //* Se crea el Policye
                var policy = new Policy(policyId, policyCoverage, policyIssueDate, policyExpirationDate, insuredVehicleId!, feeId!);

                //* Se agrega el Policye a la BD
                await _policyRepository.AddAsync(policy);

                //* Retorna la id del Policye
                return policy.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}