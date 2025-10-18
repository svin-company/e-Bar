using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface IFoodRepository
    {
        public Task<int> Add(string name, decimal price);
        public Task Update(Food food);
        public Task Delete(int id);
        public Task<IEnumerable<Food>> GetAll();
        public Task<Food> Get(string name);
    }
}
