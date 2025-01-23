using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Response;

namespace ProviderMs.Application.Queries
{
    public class GetDepartmentQuery : IRequest<GetDepartment>
    {
        public Guid Id { get; set; }

        public GetDepartmentQuery(Guid id)
        {
            Id = id;
        }
    }
}