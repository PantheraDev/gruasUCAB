using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Request;
using OrderMs.Common.Enums;

namespace OrderMs.Application.Commands
{
    public class VerifyAdditionalCostCommand : IRequest<AdditionalCostVerified>
    {
        public Guid Id { get; set; }
        public VerifyAdditionalCostCommand(Guid id)
        {
            Id = id;
        }
    }
}