using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderMs.Common.dto.Request
{
    public record UpdateDepartmentDto
    {
        public string? Name { get; init; }
    }
}