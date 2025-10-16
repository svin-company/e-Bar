using eBar.Core.Model;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        public Task<int> AddOrderItem(OrderItem item);
        public Task Delete(int id);
        public Task<IEnumerable<OrderItem>> GetAll();
        public Task<OrderItem> Get(int id);
        public Task<OrderItem> GetByFoodId(int id);
        public Task IncreaseAmount(OrderItem orderItem, int amount);
    }
}
