using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Dtos.Response
{
    public class GetIncidentDto
    {
        public Guid Id { get; set; }
        public string DestinyLocation { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string? CreatedBy { get; set; }

        public GetIncidentDto(Guid id, string? createdBy, string destinyLocation, string description, DateTime date)
        {
            Id = id;
            CreatedBy = createdBy;
            DestinyLocation = destinyLocation;
            Description = description;
            Date = date;
        }

    }
}