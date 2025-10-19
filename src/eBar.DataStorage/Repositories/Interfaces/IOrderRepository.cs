using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public  Task<int> AddAsync(int orderStatusId, int tableId);
        public Task<int> AddAsync(Order order);
        public Task DeleteAsync(int id);
        public Task<Order> GetByOrderIdAsync(int id);
        public Task<Order> ChangeStatusAsync(Order order, int statusId);
        public Task<IEnumerable<Order>> GetByTableIdAsync(int id);
        public Task AddOrderWithItemsAsync(Order order, int tableId);
        public Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int id);
    }
}
