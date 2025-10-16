using eBar.Core.Model;

namespace eBar.DataStorage.Repositories.Interfaces
{
    public interface ITableRepository
    {
        public Task<IEnumerable<Table>> GetAll();
    }
}
