using eBar.Core.Model;
using eBar.DataStorage.Exceptions;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBar.DataStorage.Services
{
    public class WaiterService : IWaiterService
    {
        private readonly IWaiterRepository _waiterRepository;

        public WaiterService(IWaiterRepository waiterRepository)
        {
            _waiterRepository = waiterRepository;
        }

        public async Task<IEnumerable<Waiter>> GetAllAsync()
        {
            var waiters = await  _waiterRepository.GetAllAsync();
            if (!waiters.Any())
            {
                throw new NoRecordsException($"Ошибка: таблица restaurant_table пустая");
            }
            return waiters;
        }

        public async Task<string> GetByIdAsync(int waiterId)
        {
            var waiterName = await _waiterRepository.GetByIdAsync(waiterId);
            if (waiterName == null)
            {
                throw new EntityDoesNotExistException("Официанта с таким Id не существует");
            }
            return waiterName;
        }
    }
}
