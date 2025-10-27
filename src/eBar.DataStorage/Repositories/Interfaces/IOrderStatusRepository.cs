using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface IOrderStatusRepository
    {
        public Task<int> AddAsync(string name);
        public Task UpdateAsync(OrderStatus status);
        public Task DeleteAsync(int id);
        public Task<IEnumerable<OrderStatus>> GetAllAsync();
        public Task<OrderStatus> GetAsync(string name);

    }
}
