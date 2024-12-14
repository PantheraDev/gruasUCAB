//TODO: Borrar todos los console, los using innecesarios y los comentarios innecesarios
using Microsoft.EntityFrameworkCore;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Database;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public OrderRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task AddAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Order?> GetByIdAsync(OrderId id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            //TODO: Borrar todos los console
            Console.WriteLine("This is the Order:" + order);
            return order;
        }
        public async Task<List<Order>?> GetAllAsync()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            return orders;
        }

        public async Task DeleteAsync(OrderId id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null) throw new OrderNotFoundException("Order not found");

            order.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Order?> UpdateAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveEfContextChanges("");
            return order;
        }
    }
}