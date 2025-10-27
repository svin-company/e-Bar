using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface IFoodRepository
    {
        public Task<int> AddAsync(string name, decimal price);
        public Task UpdateAsync(Food food);
        public Task DeleteAsync(int id);
        public Task<IEnumerable<Food>> GetAllAsync();
        public Task<Food> GetAsync(string name);
    }
}
