using MediatR;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Application.Commands
{
    //TODO: Aqui utilizo un record en lugar de clase
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Guid>
    {
        private readonly IClientRepository _clientRepository;
        public UpdateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository)); //*Valido que estas inyecciones sean exitosas
        }

        public async Task<Guid> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldClient = await _clientRepository.GetByIdAsync(ClientId.Create(request.Id)!);

                if (oldClient == null) throw new ClientNotFoundException("Client not found");


                if (request.Client.Name != null)
                {
                    oldClient = Client.Update(oldClient, ClientName.Create(request.Client.Name), null, null, null, null, null);
                }
                if (request.Client.LastName != null)
                {
                    oldClient = Client.Update(oldClient, null, ClientLastName.Create(request.Client.LastName), null, null, null, null);
                }
                if (request.Client.Ci != null)
                {
                    oldClient = Client.Update(oldClient, null, null, ClientCi.Create(request.Client.Ci), null, null, null);
                }
                if (request.Client.Phone != null)
                {
                    oldClient = Client.Update(oldClient, null, null, null, ClientPhone.Create(request.Client.Phone), null, null);
                }
                if (request.Client.Address != null)
                {
                    oldClient = Client.Update(oldClient, null, null, null, null, ClientAddress.Create(request.Client.Address), null);
                }
                if (request.Client.BirthDate != null)
                {
                    oldClient = Client.Update(oldClient, null, null, null, null, null, ClientBirthDate.Create(request.Client.BirthDate.Value.ToUniversalTime()));
                }

                await _clientRepository.UpdateAsync(oldClient);

                return oldClient.Id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}