using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Response;

namespace OrderMs.Application.Queries
{
    public class GetInsuredVehicleQuery : IRequest<GetInsuredVehicleDto>
    {
        public Guid Id { get; set; }

        public GetInsuredVehicleQuery(Guid id)
        {
            Id = id;
        }
    }
}