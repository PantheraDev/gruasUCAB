using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Dtos.Provider
{
    public class GetProvider
    {
        public Guid id { get; set; }
        public string name { get; set; }

        public GetProvider() { }
        public GetProvider(Guid id, string name)
        {
            this.name = name;
            this.id = id;
        }
    }
}