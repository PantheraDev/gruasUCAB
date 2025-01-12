using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderMs.Common.dto.Response
{
    public class GetDepartment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Createdby { get; set; }


        public GetDepartment(Guid id, string name, string? createdBy)
        {
            Id = id;
            Name = name;
            Createdby = createdBy;
        }
    }


}