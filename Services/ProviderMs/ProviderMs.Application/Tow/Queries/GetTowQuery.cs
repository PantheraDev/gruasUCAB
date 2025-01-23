using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Response;

namespace ProviderMs.Application.Queries
{
    public class GetTowQuery : IRequest<GetTow>
    {
        public Guid Id { get; set; }

        public GetTowQuery(Guid id)
        {
            Id = id;
        }
    }
}