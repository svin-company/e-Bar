using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Services.Interfaces;
using eBar.Core.Model;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
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

        public void AddFood(Order order, Food food)
        {
            if (order.OrderItems  != null)
            {
                var existingOrderItem = order.OrderItems
                    .Where(x => x.Food.Name.Equals(food.Name))
                    .FirstOrDefault();
                if (existingOrderItem == null)
                {
                    var orderItem = new OrderItem
                    {
                        Id = 1,
                        Food = food,
                        Amount = 1
                    };
                    order.OrderItems.Add(orderItem);
                }
                else
                {
                    int amount = existingOrderItem.Amount++;
                }
            }
            else
            {
                var orderItem = new OrderItem
                {
                    Id = 1, 
                    Food = food,
                    Amount = 1
                };
                order.OrderItems.Add(orderItem);
            }
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

        public void DeleteItem(Order order, OrderItem orderItem)
        {
            order.OrderItems.Remove(orderItem);
        }

        public async Task<List<Order>> GetOrdersByTableIdAsync(int id)
        {
            var selectedOrders = await _orderRepository.GetByTableIdAsync(id);
            return selectedOrders.ToList();
        }
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByOrderIdAsync(id);
        }

        public async Task<List<OrderItem>> GetItemsByIdAsync(int id)
        {
            var items = await _orderRepository.GetOrderItemsAsync(id);
            return items.ToList();
        }
    }
}
