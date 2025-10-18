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

        public async Task<List<Table>> GetAll()
        {
            var tables = await _tableRepository.GetAll();
            if (tables.Count() == 0)
            {
                throw new NoRecordsException($"Ошибка: таблица table пустая");
            }
            return tables.ToList();
        }
    }
}
