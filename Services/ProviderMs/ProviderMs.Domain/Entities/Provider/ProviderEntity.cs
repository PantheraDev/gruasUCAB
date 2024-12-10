using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Primitives;
using ProviderMs.Domain.ValueObjects;



namespace ProviderMs.Domain.Entities;

public  sealed class Provider : AggregateRoot
{
    public ProviderId Id {get; private set;}
    public ProviderName Name{get; private set;}
    public ProviderPhone Phone {get; private set;}
    public ProviderEmail Email {get; private set;}
    public ProviderRIF RIF {get; private set;}
    public ProviderAddress Address {get; private set;}
    public List<ProviderDepartament> ProviderDepartaments {get; set;}
    public List<Tow> Tows {get; set;} = new List<Tow>();



    public Provider (ProviderId id, ProviderName name, ProviderPhone phone, ProviderEmail email, ProviderRIF rif, ProviderAddress address)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
        RIF = rif;
        Address = address;
        ProviderDepartaments = new List<ProviderDepartament>();
    }

    public Provider() { }

    public static Provider Update(Provider provider, ProviderName? name, ProviderPhone? phone, ProviderEmail? email, ProviderRIF? rif, ProviderAddress? address, List<DepartamentId>? departamentIds)
{
    var updates = new List<Action>()
    {
        () => { if (name != null) provider.Name = name; },
        () => { if (phone != null) provider.Phone = phone; },
        () => { if (email != null) provider.Email = email; },
        () => { if (rif != null) provider.RIF = rif; },
        () => { if (address != null) provider.Address = address; },
() => {if(departamentIds !=null)provider.ProviderDepartaments = departamentIds.ConvertAll(id => new ProviderDepartament{ProviderId = provider.Id, DepartamentId= id});},
    };

    updates.ForEach(update => update());
    return provider;
}
    /*
    public void AddDepartament(Departament departament)
    {
       ProviderDepartaments.Add(new ProviderDepartament{
            ProviderId = Id,
            DepartamentId = departament.Id, 
            Departament = departament});
    }
    public void UpdateDepartaments (List<DepartamentId> departamentIds)
    {
        ProviderDepartaments.Clear();
        foreach (var departamentId in departamentIds)
        {
            ProviderDepartaments.Add(new ProviderDepartament
            {
                ProviderId = Id,
                DepartamentId = departamentId,
            });
        }
    }
    */
}