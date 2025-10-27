using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Services.Interfaces
{
    public interface IWaiterService
    {
        public Task<IEnumerable<Waiter>> GetAllAsync();
        public Task<string> GetByIdAsync(int waiterId);
    }
}
