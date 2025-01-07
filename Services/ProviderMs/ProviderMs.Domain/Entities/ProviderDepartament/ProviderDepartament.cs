using ProviderMs.Common.Primitives;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Domain.Entities;

public class ProviderDepartment : AggregateRoot
{
    public ProviderDepartmentId Id { get; set; }
    public ProviderId ProviderId { get; set; }
    public Provider Provider { get; set; }
    public DepartmentId DepartmentId { get; set; }
    public Department Department { get; set; }

    public ProviderDepartment(ProviderDepartmentId id, ProviderId providerId, DepartmentId departmentId)
    {
        Id = id;
        ProviderId = providerId;
        DepartmentId = departmentId;
    }

    public static ProviderDepartment Update(ProviderDepartment providerDepartment, ProviderId? providerId, DepartmentId? departmentId)
    {

        var updates = new List<Action>{
                () => {if(providerId !=null)providerDepartment.ProviderId = providerId;},
                () => {if(departmentId !=null)providerDepartment.DepartmentId = departmentId;},
            };
        updates.ForEach(update => update());
        return providerDepartment;
    }
}