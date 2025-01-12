using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Primitives;
using ProviderMs.Domain.ValueObjects;



namespace ProviderMs.Domain.Entities;

public sealed class Provider : AggregateRoot
{
    public ProviderId Id { get; private set; }
    public ProviderName Name { get; private set; }
    public ProviderPhone Phone { get; private set; }
    public ProviderEmail Email { get; private set; }
    public ProviderRIF RIF { get; private set; }
    public ProviderAddress Address { get; private set; }

    public List<Tow> Tows { get; set; } = new List<Tow>();
    //public DepartmentId DepartmentId { get; private set; }
    public List<Department> Departments { get; private set; }



    public Provider(ProviderId id, ProviderName name, ProviderPhone phone, ProviderEmail email, ProviderRIF rif, ProviderAddress address)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
        RIF = rif;
        Address = address;
    }

    public Provider() { }

    public static Provider Update(Provider provider, ProviderName? name, ProviderPhone? phone, ProviderEmail? email, ProviderRIF? rif, ProviderAddress? address, List<DepartmentId>? departmentIds)
    {
        var updates = new List<Action>()
    {
        () => { if (name != null) provider.Name = name; },
        () => { if (phone != null) provider.Phone = phone; },
        () => { if (email != null) provider.Email = email; },
        () => { if (rif != null) provider.RIF = rif; },
        () => { if (address != null) provider.Address = address; },
    };

        updates.ForEach(update => update());
        return provider;
    }
    /*
    public void AddDepartment(Department department)
    {
       ProviderDepartments.Add(new ProviderDepartment{
            ProviderId = Id,
            DepartmentId = department.Id, 
            Department = department});
    }
    public void UpdateDepartments (List<DepartmentId> departmentIds)
    {
        ProviderDepartments.Clear();
        foreach (var departmentId in departmentIds)
        {
            ProviderDepartments.Add(new ProviderDepartment
            {
                ProviderId = Id,
                DepartmentId = departmentId,
            });
        }
    }
    */
}