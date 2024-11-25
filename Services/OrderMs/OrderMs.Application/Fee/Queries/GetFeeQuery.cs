using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Response;

namespace OrderMs.Application.Queries
{
    public class GetFeeQuery : IRequest<GetFeeDto>
    {
        public Guid Id { get; set; }

        public GetFeeQuery(Guid id)
        {
            Id = id;
        }
    }
}