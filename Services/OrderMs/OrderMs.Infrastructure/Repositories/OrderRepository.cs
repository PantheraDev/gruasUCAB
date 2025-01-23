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

        public async Task<List<AdditionalCost>?> FindAdditionalCosts(OrderId id)
        {
            var addCost = await _dbContext.AdditionalCosts.Where(x => x.OrderId == id && !x.IsDeleted).ToListAsync();
            return addCost;
        }

        public async Task<Order?> GetByIdAsync(OrderId id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            var addCosts = await FindAdditionalCosts(id);
            if (addCosts != null)
            {
                foreach (var addCost in addCosts)
                {
                    order?.AddAdditionalCost(addCost!);
                }
            }
            return order;
        }

        public async Task<List<Order>?> GetAllAsync()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            
            foreach (var order in orders)
            {
                
                var addCosts = await FindAdditionalCosts(order.Id);
                if (addCosts != null)
                {
                    foreach (var addCost in addCosts)
                    {
                        order.AddAdditionalCost(addCost!);
                    }
                }
                Console.WriteLine(order.AdditionalCosts.Count);
            }
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