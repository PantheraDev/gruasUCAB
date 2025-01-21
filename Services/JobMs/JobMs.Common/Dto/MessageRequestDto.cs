using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobMs.Common.Dto
{
    public class MessageRequestDto
    {
        public Guid Id {get; set; }
        public string? DeviceToken { get; set; }

    }
}