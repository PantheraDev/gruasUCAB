using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Response;

namespace OrderMs.Application.Queries
{
    public class GetOrderQuery : IRequest<GetOrderDto>
    {
        public Guid Id { get; set; }

        public GetOrderQuery(Guid id)
        {
            Id = id;
        }
    }
}