using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Services.Interfaces;
using eBar.Core.Model;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace eBar.DataStorage.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<Order> UpdateStatusAsync(Order order)
        {
            order.IsOrderOpen = !order.IsOrderOpen;
            return await _orderRepository.ChangeStatusAsync(order, order.IsOrderOpen);
        }

        public async Task<bool> AddOrderAsync(Order order, int tableId, int waiterId)
        {
            if (order.OrderItems.Count == 0)
                return false;

            await _orderRepository.AddOrderWithItemsAsync(order, tableId, waiterId);
            return true;
        }

        public async Task<IEnumerable<Order>> GetOrdersByTableIdAsync(int id)
        {
            var selectedOrders = await _orderRepository.GetByTableIdAsync(id);
            return selectedOrders;
        }
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByOrderIdAsync(id);
        }

        public async Task<List<OrderItem>> GetItemsByIdAsync(int id)
        {
            var items = await _orderItemRepository.GetItemsByOrderIdAsync(id);
            return items.ToList();
        }

        public async Task<bool> DeleteOrderAsync(Order order)
        {
            if (order == null)
                return false;

            var selectedOrder = await _orderRepository.GetByOrderIdAsync(order.Id);

            if (selectedOrder != null)
                return await _orderRepository.DeleteAsync(selectedOrder.Id);

            return false;
        }

        public async Task <IEnumerable<OrderItem>?> UpdateOrderItemsAsync(Order order)
        {
            var result = await _orderItemRepository.UpdateItemsAsync(order.OrderItems);

            if (result)
                return await GetItemsByIdAsync(order.Id);

            return null;
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            await _orderItemRepository.DeleteAsync(id);
        }

        public async Task AddOrderItemAsync(OrderItem item)
        {
             await _orderItemRepository.AddAsync(item);
        }
    }
}
