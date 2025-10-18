using eBar.DataStorage.Exceptions;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Services.Interfaces;
using eBar.Core.Model;
using System.Threading.Tasks;

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

        public async Task AddFood(int orderId, Food food)
        {
            var existingOrder = await _orderRepository.GetByOrderId(orderId);
            if (existingOrder == null)
            {
                throw new EntityDoesNotExistException("Ошибка добавления товара, такого заказа не существует");
            }
            else
            {
                var existingOrderItem = await _orderItemRepository.GetByFoodId(food.Id);
                if (existingOrderItem == null)
                {
                    var orderItem = new OrderItem(1, food.Id, orderId);
                    await _orderItemRepository.AddOrderItem(orderItem);
                }
                else
                {
                    int amount = existingOrderItem.Amount++;
                    await _orderItemRepository.IncreaseAmount(existingOrderItem, amount);
                }
            }
        }

        public async Task DecreaseItemAmount(OrderItem item)
        {
            var existingItem = await _orderItemRepository.Get(item.Id);
            if (existingItem == null)
            {
                throw new EntityDoesNotExistException("Ошибка уменьшения количества: Элемент заказа не существует");
            }
            else
            {
                if (item.Amount == 1)
                {
                    await DeleteItem(item);
                }
                else
                {
                    int amount = item.Amount--;
                    await _orderItemRepository.IncreaseAmount(item, amount);
                }
            }
        }

        public async Task DeleteItem(OrderItem item)
        {
            await _orderItemRepository.Delete(item.Id);
        }

        public async Task IncreaseItemAmount(OrderItem item)
        {
            var existingItem= await _orderItemRepository.Get(item.Id);
            if (existingItem == null)
            {
                throw new EntityDoesNotExistException("Ошибка увеличения количества: Элемент заказа не существует");
            }
            else
            {
                int amount = item.Amount++;
                await _orderItemRepository.IncreaseAmount(item, amount);
            }
        }
    }
}
