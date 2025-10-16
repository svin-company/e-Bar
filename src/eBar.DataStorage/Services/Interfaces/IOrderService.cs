using eBar.Core.Model;

namespace eBar.DataStorage.Services.Interfaces
{
    public interface IOrderService
    {
        public Task AddFood(int orderId, Food food);
        public Task DecreaseItemAmount(OrderItem item);
        public Task IncreaseItemAmount(OrderItem item);
        public Task DeleteItem(OrderItem item);
    }
}
