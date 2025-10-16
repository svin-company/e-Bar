using eBar.Core.Model;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public  Task<int> Add(int orderStatusId, int tableId);
        public Task Delete(int id);
        public Task<Order> GetByOrderId(int id);
        public Task ChangeOrderStatus(Order order, int statusId);
        public Task<IEnumerable<Order>> GetByTableId(int id);
    }
}
