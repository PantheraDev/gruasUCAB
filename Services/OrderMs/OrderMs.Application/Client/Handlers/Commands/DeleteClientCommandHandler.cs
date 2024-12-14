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
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Guid>
    {
        private readonly IClientRepository _clientRepository;
        public DeleteClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository)); //*Valido que estas inyecciones sean exitosas
            
        }

        public async Task<Guid> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var clientId = ClientId.Create(request.ClientId);
                await _clientRepository.DeleteAsync(clientId!);
                return clientId!.Value;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}