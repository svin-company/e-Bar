using eBar.DataStorage.Exceptions;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.DataStorage.Services.Interfaces;
using eBar.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace eBar.DataStorage.Services
{
    public class TableService: ITableService
    {
        private readonly ITableRepository _tableRepository;

        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
        {
            var tables = await _tableRepository.GetAllAsync();
            if (tables.Count() == 0)
            {
                throw new NoRecordsException($"Ошибка: таблица restaurant_table пустая");
            }
            return tables;
        }
    }
}
