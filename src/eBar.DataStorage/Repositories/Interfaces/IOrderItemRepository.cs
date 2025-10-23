using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        public Task<IEnumerable<OrderItem>> GetAllAsync();
    }
}
