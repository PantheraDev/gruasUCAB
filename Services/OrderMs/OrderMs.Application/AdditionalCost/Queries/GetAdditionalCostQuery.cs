using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Response;

namespace OrderMs.Application.Queries
{
    public class GetAdditionalCostQuery : IRequest<GetAdditionalCostDto>
    {
        public Guid Id { get; set; }

        public GetAdditionalCostQuery(Guid id)
        {
            Id = id;
        }
    }
}