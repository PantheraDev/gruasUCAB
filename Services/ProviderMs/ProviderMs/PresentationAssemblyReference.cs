using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProviderMs
{
    public class PresentationAssemblyReference
    {
        internal static readonly Assembly Assembly = typeof(PresentationAssemblyReference).Assembly;
    }
}