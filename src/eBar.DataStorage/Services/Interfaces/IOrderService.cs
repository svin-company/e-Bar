using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> UpdateStatusAsync(Order order);
        public Task<bool> AddOrderAsync(Order order, int tableId, int waiterId);
        public Task<IEnumerable<Order>> GetOrdersByTableIdAsync(int id);
        public Task<Order> GetOrderByIdAsync(int id);
        public Task<bool> DeleteOrderAsync(Order order);
        public Task <IEnumerable<OrderItem>> UpdateOrderItemsAsync(Order order);

        public Task  DeleteOrderItemAsync(int id);
        public Task AddOrderItemAsync(OrderItem item);
        public Task<List<OrderItem>> GetItemsByIdAsync(int id);

    }
}
