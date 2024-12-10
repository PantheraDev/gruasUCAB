using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProviderMs.Domain.ValueObjects
{
    public class TowAvailibility : IEquatable<TowAvailibility>
    {
        public bool Value { get; }

        private TowAvailibility(bool value)
        {
            Value = value;
        }

        public static TowAvailibility Create(bool value)
        {
            // Aquí puedes agregar cualquier lógica de validación adicional si es necesario
            return new TowAvailibility(value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TowAvailibility);
        }

        public bool Equals(TowAvailibility other)
        {
            return other != null && Value == other.Value;
        }
         public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator bool(TowAvailibility availability)
        {
            return availability.Value;
        }

        public static explicit operator TowAvailibility(bool value)
        {
            return Create(value);
        }
    }
}