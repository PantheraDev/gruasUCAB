using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Dtos.Request
{
    public record CreateClientDto
    {
        public string Name { get; init; }
        public string LastName { get; init; }
        public string Ci { get; init; }
        public string Phone { get; init; }
        public string Address { get; init; }
        public DateTime BirthDate { get; init; }

    }
}