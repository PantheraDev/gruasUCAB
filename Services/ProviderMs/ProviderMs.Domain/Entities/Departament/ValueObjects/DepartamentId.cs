using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Domain.ValueObjects
{
    public class DepartamentId

    {
        private DepartamentId(Guid value) => Value = value;

        public static DepartamentId Create()
        {
            return new DepartamentId(Guid.NewGuid());
        }
        public static DepartamentId? Create(Guid value)
        {
            if (value == Guid.Empty) throw new NullAttributeException("Departament id is required");

            return new DepartamentId(value);
        }

        public Guid Value { get; init; }
    }
}