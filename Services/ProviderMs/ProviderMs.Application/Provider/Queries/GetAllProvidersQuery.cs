using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Response;
using ProviderMs.Domain.Entities;

namespace ProviderMs.ApplicationQueries
{
    public class GetAllProvidersQuery : IRequest<List<GetProvider>>
    {
        public GetAllProvidersQuery() { }
    }
}