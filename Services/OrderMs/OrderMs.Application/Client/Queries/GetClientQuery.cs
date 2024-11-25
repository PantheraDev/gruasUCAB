using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Response;

namespace OrderMs.Application.Queries
{
    public class GetClientQuery : IRequest<GetClientDto>
    {
        public Guid Id { get; set; }

        public GetClientQuery(Guid id)
        {
            Id = id;
        }
    }
}