using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderMs.Common.Primitives
{
    public class AggregateRoot
    {

        public DateTime CreatedAt {get; set;}
        public string? CreatedBy {get; set;}
        public DateTime? UpdatedAt {get; set;}
        public string? UpdatedBy {get; set;}
        public bool IsDeleted {get; set; } = false; 
        private readonly List<DomainEvent> _domainevent = new();

        public ICollection<DomainEvent> GetDomainEvents() => _domainevent;

        protected void Raise(DomainEvent domainEvent)
        {
            _domainevent.Add(domainEvent);
        }
    }
}