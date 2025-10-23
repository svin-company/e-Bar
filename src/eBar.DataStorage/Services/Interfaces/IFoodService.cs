using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Services.Interfaces
{
    public interface IFoodService
    {
        public Task<int> AddAsync(string name, decimal price);
        public Task DeleteAsync(string name);
        public Task UpdateAsync(string name, decimal newPrice);
        public Task UpdateAsync(string oldName, string newName);
        public Task<Food> GetAsync(string name);
        public Task<List<Food>> GetAllAsync();
    }
}
