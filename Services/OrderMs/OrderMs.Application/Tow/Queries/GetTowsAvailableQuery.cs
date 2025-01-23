using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dto.Response;
using OrderMs.Common.Dtos.Response;
using OrderMs.Domain.Entities;

namespace OrderMs.ApplicationQueries
{
    public class GetTowsAvailableQuery : IRequest<List<TowsAvaliable>>
    {
        public Guid orderId;
        public GetTowsAvailableQuery(Guid orderId)
        {
            this.orderId = orderId;
        }
    }
}