using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Dtos.Request;
using OrderMs.Common.Primitives;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Domain.Entities
{
    //* Porque es sellada? Porque no necesito decir que esta clase tenga una modificacion externa y que todo su comportamiento este dentro de ella

    public sealed class Client : AggregateRoot
    {
        public ClientId Id { get; private set; }
        public ClientName Name { get; private set; }
        public ClientLastName LastName { get; private set; }
        public ClientCi Ci { get; private set; }
        public ClientPhone Phone { get; private set; }
        public ClientAddress Address { get; private set; }
        public ClientBirthDate BirthDate { get; private set; }

        public Client(ClientId id, ClientName name, ClientLastName lastName, ClientCi ci, ClientPhone phone, ClientAddress address, ClientBirthDate birthDate)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Ci = ci;
            Phone = phone;
            Address = address;
            BirthDate = birthDate;
        }

        public Client() { }

        public static Client Update(Client client, ClientName? name, ClientLastName? lastName, ClientCi? ci, ClientPhone? phone, ClientAddress? address, ClientBirthDate? birthDate)
        {
            var updates = new List<Action>{
                    () => { if (name != null) client.Name = name; },
                    () => { if (lastName != null) client.LastName = lastName; },
                    () => { if (ci != null) client.Ci = ci; },
                    () => { if (phone != null) client.Phone = phone; },
                    () => { if (address != null) client.Address = address; },
                    () => { if (birthDate != null) client.BirthDate = birthDate; }
                };

            updates.ForEach(update => update());
            return client;
        }
    }
}