using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Services.Interfaces
{
    public interface IFoodService
    {
        public Task<int> AddFood(string name, decimal price);
        public Task DeleteFood(string name);
        public Task UpdateFood(string name, decimal newPrice);
        public Task UpdateFood(string oldName, string newName);
        public Task<Food> GetFood(string name);
        public Task<List<Food>> GetAllFood();
    }
}
