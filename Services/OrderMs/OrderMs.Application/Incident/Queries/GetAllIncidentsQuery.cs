using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderMs.Common.Dtos.Response;
using OrderMs.Domain.Entities;

namespace OrderMs.ApplicationQueries
{
    public class GetAllIncidentsQuery : IRequest<List<GetIncidentDto>>
    {
        public GetAllIncidentsQuery() { }
    }
}