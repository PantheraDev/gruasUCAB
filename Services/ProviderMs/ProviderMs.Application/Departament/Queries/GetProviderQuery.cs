using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Response;

namespace ProviderMs.Application.Queries
{
    public class GetDepartamentQuery : IRequest<GetDepartament>
    {
        public Guid Id { get; set; }

        public GetDepartamentQuery(Guid id)
        {
            Id = id;
        }
    }
}