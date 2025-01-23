using OrderMs.Common.Primitives;
using OrderMs.Domain.Entities.ValueObjects;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Domain.Entities
{
    //* Porque es sellada? Porque no necesito decir que esta clase tenga una modificacion externa y que todo su comportamiento este dentro de ella

    public sealed class Incident : AggregateRoot
    {
        public IncidentId Id { get; private set; }
        public IncidentDescription Description { get; private set; }
        public IncidentDestinyLocation DestinyLocation { get; private set; }
        public IncidentDate Date { get; private set; }

        public Incident(IncidentId id, IncidentDescription description, IncidentDestinyLocation destinyLocation, IncidentDate date)
        {
            Id = id;
            Description = description;
            DestinyLocation = destinyLocation;
            Date = date;
        }

        public Incident() { }

        public static Incident Update(Incident Incident, IncidentDescription? description, IncidentDestinyLocation? destinyLocation, IncidentDate? date)
        {
            // TODO: Esto podria solucionarse haciendo un DTO
            var updates = new List<Action>{
                    () => { if (description != null) Incident.Description = description; },
                    () => { if (destinyLocation != null) Incident.DestinyLocation = destinyLocation; },
                    () => { if (date != null) Incident.Date = date; }
                };

            updates.ForEach(update => update());
            return Incident;
        }
    }
}