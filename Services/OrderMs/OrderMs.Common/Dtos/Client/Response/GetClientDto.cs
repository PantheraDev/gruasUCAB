using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Dtos.Response
{
    public class GetClientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Ci { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string? CreatedBy { get; set; }

        public GetClientDto(Guid id, string name, string lastName, string ci, string phone, string address, DateTime birthDate, string? createdBy)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Ci = ci;
            Phone = phone;
            Address = address;
            BirthDate = birthDate;
            CreatedBy = createdBy;
        }

    }
}