using ProviderMs.Common.Primitives;
using ProviderMs.Domain.ValueObjects;




namespace ProviderMs.Domain.Entities;

public  sealed class Departament : AggregateRoot
{
    public DepartamentId Id {get; private set;}
    public DepartamentName Name{get; private set;}

    public List<ProviderDepartament> ProviderDepartaments {get; private set;}

    public Departament (DepartamentId id, DepartamentName name)
    {
        Id = id;
        Name = name;
        ProviderDepartaments = new List<ProviderDepartament>();
    }

    public Departament() { }

    public static Departament Update(Departament departament, DepartamentName? name){

            var updates = new List<Action>{
                () => {if(name !=null) departament.Name = name;},
            };
            updates.ForEach(update => update());
            return departament;
    }
}