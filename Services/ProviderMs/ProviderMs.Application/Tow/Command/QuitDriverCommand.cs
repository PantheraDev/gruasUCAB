using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ProviderMs.Common.dto.Request;

namespace ProviderMs.Application.Command
{
    public class QuitDriverCommand : IRequest<string>
    {
        public Guid TowId { get; set; }

        public QuitDriverCommand(Guid towId)
        {
            TowId = towId;
        }
    }
}