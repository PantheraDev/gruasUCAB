using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Response;

namespace OrderMs.Application.Queries
{
    public class GetIncidentQuery : IRequest<GetIncidentDto>
    {
        public Guid Id { get; set; }

        public GetIncidentQuery(Guid id)
        {
            Id = id;
        }
    }
}