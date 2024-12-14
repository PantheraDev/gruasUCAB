using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Application.Validators;
using OrderMs.Common.Primitives;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Guid>
    {
        private readonly IClientRepository _clientRepository;
        public CreateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //TODO: Revisar este validador
                var validator = new CreateClientValidator();
                await validator.ValidateRequest(request.Client);

                //* Se crean los Value Objects
                var clientId = ClientId.Create();
                var clientName = ClientName.Create(request.Client.Name);
                var clientLastName = ClientLastName.Create(request.Client.LastName);
                var clientCi = ClientCi.Create(request.Client.Ci);
                var clientPhone = ClientPhone.Create(request.Client.Phone);
                var clientAddress = ClientAddress.Create(request.Client.Address);
                var clientBirthDate = ClientBirthDate.Create(request.Client.BirthDate.ToUniversalTime());

                //* Se crea el cliente
                var client = new Client(clientId, clientName, clientLastName, clientCi, clientPhone, clientAddress, clientBirthDate);

                //* Se agrega el cliente a la BD
                await _clientRepository.AddAsync(client);

                //* Retorna la id del cliente
                return client.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}