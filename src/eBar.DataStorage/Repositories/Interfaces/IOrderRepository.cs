using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<int> AddAsync(Order order);
        public Task DeleteAsync(int id);
        public Task<Order> GetByOrderIdAsync(int id);
        public Task<Order> ChangeStatusAsync(Order order, bool orderStatus);
        public Task<IEnumerable<Order>> GetByTableIdAsync(int id);
        public Task AddOrderWithItemsAsync(Order order, int tableId, int waiterId);
        public Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int id);
        public Task<int> GetStatusId(bool orderStatus);
    }
}
