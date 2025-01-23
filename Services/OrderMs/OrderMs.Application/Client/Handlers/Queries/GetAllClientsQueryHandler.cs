using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Queries;
using OrderMs.ApplicationQueries;
using OrderMs.Common.Dtos.Request;
using OrderMs.Common.Dtos.Response;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Handlers.Queries
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, List<GetClientDto>>
    {
        public IClientRepository _clientRepository;

        public GetAllClientsQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<GetClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetAllAsync();

            if (client == null) throw new ClientNotFoundException("Clients are empty");

            return client.Where(c => !c.IsDeleted).Select(c =>
                new GetClientDto(
                    c.Id.Value,
                    c.Name.Value,
                    c.LastName.Value,
                    c.Ci.Value,
                    c.Phone.Value,
                    c.Address.Value,
                    c.BirthDate.Value,
                    c.CreatedBy
                )
            ).ToList();
        }
    }
}