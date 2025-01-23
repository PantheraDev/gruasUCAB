using ProviderMs.Common.Primitives;
using ProviderMs.Domain.ValueObjects;




namespace ProviderMs.Domain.Entities;

public sealed class Department : AggregateRoot
{
    public DepartmentId Id { get; private set; }
    public DepartmentName Name { get; private set; }

    //public List<ProviderDepartment> ProviderDepartments {get; private set;}
    //public ProviderId ProviderId { get; private set; }
    public List<Provider> Providers { get; private set; }

    public Department(DepartmentId id, DepartmentName name)
    {
        Id = id;
        Name = name;
        //ProviderDepartments = new List<ProviderDepartment>();
    }

    public Department() { }

    public static Department Update(Department department, DepartmentName? name)
    {

        var updates = new List<Action>{
                () => {if(name !=null) department.Name = name;},
            };
        updates.ForEach(update => update());
        return department;
    }
}