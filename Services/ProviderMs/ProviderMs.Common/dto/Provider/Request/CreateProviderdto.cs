using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderMs.Common.dto.Request
{
    public record CreateProviderdto
    {   
        public string? Name { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Email { get; init; }
        public string? RIF { get; init; }
        public string? Address { get; init; }
        public List<Guid> DepartamentId { get; init; }
    }
}