using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        public Task<IEnumerable<OrderItem>> GetAllAsync();
        public Task<IEnumerable<OrderItem>> GetItemsByOrderIdAsync(int id);

        public Task DeleteAsync(int id);
        public Task <bool> UpdateItemsAsync(List<OrderItem> items);
        public Task AddAsync(OrderItem item);
    }
}
