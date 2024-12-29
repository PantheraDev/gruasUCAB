using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using ProviderMs.Common.Primitives;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Domain.Entities;

    public class ProviderDepartament : AggregateRoot
    {
        public ProviderId ProviderId {get; set;}
        public Provider Provider {get; set;}
        public DepartamentId DepartamentId {get;  set;}
        public Departament Departament {get; set;}

        public static ProviderDepartament Update(ProviderDepartament providerDepartament, DepartamentId? departamentId){

            var updates = new List<Action>{
                () => {if(departamentId !=null)providerDepartament.DepartamentId = departamentId;},
            };
            updates.ForEach(update => update());
            return providerDepartament;
        }
    }