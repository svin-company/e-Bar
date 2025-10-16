using eBar.Core.Model;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface IOrderStatusRepository
    {
        public Task<int> Add(string name);
        public Task Update(OrderStatus status);
        public Task Delete(int id);
        public Task<IEnumerable<OrderStatus>> GetAll();
        public Task<OrderStatus> Get(string name);

    }
}
