using eBar.DataStorage.Exceptions;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Services.Interfaces;
using eBar.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace eBar.DataStorage.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;

        public FoodService(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<int> AddFood(string name, decimal price)
        {
            var existingFood = await _foodRepository.Get(name);
            if (existingFood != null)
            {
                throw new EntityExistsException($"Ошибка добавления: Продукт с именем {name} уже существует");
            }
            return await _foodRepository.Add(name, price);
        }

        public async Task DeleteFood(string name)
        {
            var existingFood = await _foodRepository.Get(name) ?? 
                throw new EntityDoesNotExistException($"Ошибка удаления: Продукта с именем {name} не существует");
            await _foodRepository.Delete(existingFood.Id);
        }

        public async Task<List<Food>> GetAllFood()
        {
            var foods = await _foodRepository.GetAll();
            if (foods.Count() ==0)
            {
                throw new NoRecordsException($"Ошибка: таблица food пустая");
            }
            return foods.ToList();
        }

        public async Task<Food> GetFood(string name)
        {
            var existingFood = await _foodRepository.Get(name);
            return existingFood ?? 
                throw new EntityDoesNotExistException($"Ошибка удаления: Продукта с именем {name} не существует");
        }

        public async Task UpdateFood(string oldName, string newName)
        {
            var existingFood = await _foodRepository.Get(oldName) ?? 
                throw new EntityDoesNotExistException($"Ошибка удаления: Продукта с именем {oldName} не существует");
            var food = new Food(existingFood.Id, newName, existingFood.Price);
            await _foodRepository.Update(food);
        }

        public async Task UpdateFood(string name, decimal newPrice)
        {
            var existingFood = await _foodRepository.Get(name) ?? 
                throw new EntityDoesNotExistException($"Ошибка изменения: Продукта с именем {name} не существует");
            var food = new Food(existingFood.Id, existingFood.Name, newPrice);
            await _foodRepository.Update(food);
        }
    }
}
