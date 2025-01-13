using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Core.Services
{
    public interface IUserService
    {
        Task<bool> DriverExists(TowDriver driver);
        
    }
}