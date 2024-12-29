using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Application.Queries;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Repository;
using ProviderMs.Domain;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetDepartamentQueryHandler : IRequestHandler<GetDepartamentQuery, GetDepartament>
    {
        public IDepartamentRepository _departamentRepository;

        public GetDepartamentQueryHandler(IDepartamentRepository departamentRepository)
        {
            _departamentRepository = departamentRepository;
        }

        public async Task<GetDepartament> Handle(GetDepartamentQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("departament id is required");
            var departamentId = DepartamentId.Create(request.Id);
            var departament = await _departamentRepository.GetByIdAsync(departamentId!);

            if (departament == null || departament.IsDeleted) throw new ProviderNotFoundException("departament not found");

            return new GetDepartament(
                departament.Id.Value,
                departament.Name.Value,
                departament.CreatedBy
            );
        }
    }
}