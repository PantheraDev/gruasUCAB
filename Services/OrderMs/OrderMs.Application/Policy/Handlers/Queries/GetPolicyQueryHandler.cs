using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Queries;
using OrderMs.Common.Dtos.Response;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Handlers.Queries
{
    public class GetPolicyQueryHandler : IRequestHandler<GetPolicyQuery, GetPolicyDto>
    {
        public IPolicyRepository _policyRepository;

        public GetPolicyQueryHandler(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task<GetPolicyDto> Handle(GetPolicyQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("Policy id is required");
            var policyId = PolicyId.Create(request.Id);
            var policy = await _policyRepository.GetByIdAsync(policyId!);
            if (policy == null || policy.IsDeleted) throw new PolicyNotFoundException("Policy not found");

            return new GetPolicyDto(
                    policy.Id.Value,
                    policy.CreatedBy,
                    policy.Coverage.Value,
                    policy.ExpirationDate.Value,
                    policy.IssueDate.Value,
                    policy.InsuredVehicleId.Value,
                    policy.FeeId.Value
                );
        }
    }
}