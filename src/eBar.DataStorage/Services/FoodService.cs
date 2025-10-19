using eBar.DataStorage.Exceptions;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Services.Interfaces;
using eBar.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace eBar.DataStorage.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;

        public FoodService(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<int> AddAsync(string name, decimal price)
        {
            var existingFood = await _foodRepository.GetAsync(name);
            if (existingFood != null)
            {
                throw new EntityExistsException($"Ошибка добавления: Продукт с именем {name} уже существует");
            }
            return await _foodRepository.AddAsync(name, price);
        }

        public async Task DeleteAsync(string name)
        {
            var existingFood = await _foodRepository.GetAsync(name) ?? 
                throw new EntityDoesNotExistException($"Ошибка удаления: Продукта с именем {name} не существует");
            await _foodRepository.DeleteAsync(existingFood.Id);
        }

        public async Task<List<Food>> GetAllAsync()
        {
            var foods = await _foodRepository.GetAllAsync();
            if (foods.Count() ==0)
            {
                throw new NoRecordsException($"Ошибка: таблица food пустая");
            }
            return foods.ToList();
        }

        public async Task<Food> GetAsync(string name)
        {
            var existingFood = await _foodRepository.GetAsync(name);
            return existingFood ?? 
                throw new EntityDoesNotExistException($"Ошибка удаления: Продукта с именем {name} не существует");
        }

        public async Task UpdateAsync(string oldName, string newName)
        {
            var existingFood = await _foodRepository.GetAsync(oldName) ?? 
                throw new EntityDoesNotExistException($"Ошибка удаления: Продукта с именем {oldName} не существует");
            var food = new Food(existingFood.Id, newName, existingFood.Price);
            await _foodRepository.UpdateAsync(food);
        }

        public async Task UpdateAsync(string name, decimal newPrice)
        {
            var existingFood = await _foodRepository.GetAsync(name) ?? 
                throw new EntityDoesNotExistException($"Ошибка изменения: Продукта с именем {name} не существует");
            var food = new Food(existingFood.Id, existingFood.Name, newPrice);
            await _foodRepository.UpdateAsync(food);
        }
    }
}
