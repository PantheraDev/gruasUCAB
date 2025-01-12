using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderMs.Common.dto.Response
{
    public class GetProvider
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RIF { get; set; }
        public string Address { get; set; }
        public string? Createdby {get; set;}
        


        public GetProvider(Guid id, string name, string phoneNumber, string email, string rif, string address, string? createdBy)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            RIF = rif;
            Address = address;
            Createdby = createdBy;
        }
    }

}