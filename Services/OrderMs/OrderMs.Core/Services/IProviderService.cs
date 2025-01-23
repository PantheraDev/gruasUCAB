
using OrderMs.Common.Dto.Response;
using OrderMs.Common.Dtos.Provider;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Core.Services
{
    public interface IProviderService
    {
        Task<List<GetTow?>> GetTowsAsync();
        Task<GetTow?> GetTowByIdAsync(TowId towId);
        Task<List<GetProvider?>> GetProviderAsync();
    }
}