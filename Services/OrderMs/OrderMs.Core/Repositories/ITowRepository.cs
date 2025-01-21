using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Common.Dto.Response;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Core.Repositories                                 //Y AQUI EL OTRO REPOSITORIO
{
    public interface ITowRepository
    {
        Task<GetTow?> GetTowByIdAsync(TowId towId);
    }
}