using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(OrderId id);
        Task AddAsync(Order order);
        Task DeleteAsync(OrderId id);
        Task<List<Order>?> GetAllAsync();
        Task<Order?> UpdateAsync(Order order);
    }
}