using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobMs.Common.Primitives
{
    public abstract class AggregateRoot
    {
        //****** BASE CLASS *************//
        //public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        //****** BASE CLASS *************//
        private List<DomainEvent> _domainEvents = new();

        //*Recolecta todos los eventos de dominios que tenga el aggregate
        public ICollection<DomainEvent> GetDomainEvents() => _domainEvents;

        //*Nos permitira ejecutar nuestro evento de dominio
        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

    }
}