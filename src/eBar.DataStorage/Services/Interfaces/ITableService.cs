using eBar.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace eBar.DataStorage.Services.Interfaces
{
    public interface ITableService
    {
        public Task<List<Table>> GetAll();
    }
}
