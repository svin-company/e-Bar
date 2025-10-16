using eBar.Core.Model;
namespace eBar.DataStorage.Services.Interfaces
{
    public interface ITableService
    {
        public Task<List<Table>> GetAll();
    }
}
