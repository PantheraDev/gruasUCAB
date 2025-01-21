using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Dtos.Provider;
//Y AQUI SUS REPOSITORIOS
namespace OrderMs.Core.Repositories
{
    public interface INotificationRepository
    {
        Task SendNotificationAsync (Guid id);
    }
}