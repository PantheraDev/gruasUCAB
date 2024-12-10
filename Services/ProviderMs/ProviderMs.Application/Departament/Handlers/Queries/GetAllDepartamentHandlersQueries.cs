using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.ApplicationQueries;
using ProviderMs.Common.dto.Request;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetAllDepartamentQueryHandler : IRequestHandler<GetAllDepartamentsQuery, List<GetDepartament>>
    {
        public IDepartamentRepository _departamentRepository;

        public GetAllDepartamentQueryHandler(IDepartamentRepository departamentRepository)
        {
            _departamentRepository = departamentRepository;
        }

        public async Task<List<GetDepartament>> Handle(GetAllDepartamentsQuery request, CancellationToken cancellationToken)
        {
            var departament = await _departamentRepository.GetAllAsync();

            if (departament == null) throw new ProviderNotFoundException("departament are empty");

            return departament.Where(p => !p.IsDeleted).Select(p =>
                new GetDepartament(
                    p.Id.Value,
                    p.Name.Value,
                    p.CreatedBy
                )
            ).ToList();
        }
    }
}