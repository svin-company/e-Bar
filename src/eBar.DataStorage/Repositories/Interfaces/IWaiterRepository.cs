using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface IWaiterRepository
    {
        public Task<IEnumerable<Waiter>> GetAllAsync();
        public Task<string> GetByIdAsync(int waiterId);
    }
}
