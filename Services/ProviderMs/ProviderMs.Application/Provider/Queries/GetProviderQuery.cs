using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Response;

namespace ProviderMs.Application.Queries
{
    public class GetProviderQuery : IRequest<GetProvider>
    {
        public Guid Id { get; set; }

        public GetProviderQuery(Guid id)
        {
            Id = id;
        }
    }
}