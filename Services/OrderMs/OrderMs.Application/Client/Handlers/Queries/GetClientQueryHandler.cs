using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Queries;
using OrderMs.Common.Dtos.Response;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Handlers.Queries
{
    public class GetClientQueryHandler : IRequestHandler<GetClientQuery, GetClientDto>
    {
        public IClientRepository _clientRepository;

        public GetClientQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<GetClientDto> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("Client id is required");
            var clientId = ClientId.Create(request.Id);
            var client = await _clientRepository.GetByIdAsync(clientId!);

            if (client == null || client.IsDeleted) throw new ClientNotFoundException("Client not found");

            return new GetClientDto(
                client.Id.Value,
                client.Name.Value,
                client.LastName.Value,
                client.Ci.Value,
                client.Phone.Value,
                client.Address.Value,
                client.BirthDate.Value,
                client.CreatedBy
            );
        }
    }
}