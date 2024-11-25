using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Queries;
using OrderMs.ApplicationQueries;
using OrderMs.Common.Dtos.Request;
using OrderMs.Common.Dtos.Response;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Handlers.Queries
{
    public class GetAllPolicysQueryHandler : IRequestHandler<GetAllPolicysQuery, List<GetPolicyDto>>
    {
        public IPolicyRepository _policyRepository;

        public GetAllPolicysQueryHandler(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task<List<GetPolicyDto>> Handle(GetAllPolicysQuery request, CancellationToken cancellationToken)
        {
            var policies = await _policyRepository.GetAllAsync();

            if (policies == null) throw new PolicyNotFoundException("Policies are empty");

            return policies.Where(c => !c.IsDeleted).Select(c =>
                new GetPolicyDto(
                    c.Id.Value,
                    c.CreatedBy,
                    c.Coverage.Value,
                    c.ExpirationDate.Value,
                    c.IssueDate.Value,
                    c.InsuredVehicleId.Value,
                    c.FeeId.Value
                )
            ).ToList();
        }
    }
}