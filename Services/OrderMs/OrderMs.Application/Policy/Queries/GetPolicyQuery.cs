using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Response;

namespace OrderMs.Application.Queries
{
    public class GetPolicyQuery : IRequest<GetPolicyDto>
    {
        public Guid Id { get; set; }

        public GetPolicyQuery(Guid id)
        {
            Id = id;
        }
    }
}