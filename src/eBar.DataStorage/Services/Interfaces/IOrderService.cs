using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Services.Interfaces
{
    public interface IOrderService
    {
        public void AddFood(Order order, Food food);
        public Task<Order> UpdateStatusAsync(Order order);
        public Task<bool> AddOrderAsync(Order order, int tableId, int waiterId);
        public void DeleteItem(Order order, OrderItem orderItem);
        public Task<List<Order>> GetOrdersByTableIdAsync(int id);
        public  Task<Order> GetOrderByIdAsync(int id);
        public Task<List<OrderItem>> GetItemsByIdAsync(int id);
    }
}
