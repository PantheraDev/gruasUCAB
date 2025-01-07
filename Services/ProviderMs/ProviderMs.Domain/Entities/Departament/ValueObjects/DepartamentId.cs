using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public class DepartmentId

    {
        private DepartmentId(Guid value) => Value = value;

        public static DepartmentId Create()
        {
            return new DepartmentId(Guid.NewGuid());
        }
        public static DepartmentId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("Department id is required");

            return new DepartmentId(value);
        }

        public Guid Value { get; init; }
    }
}