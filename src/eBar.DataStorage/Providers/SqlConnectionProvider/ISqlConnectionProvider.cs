using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Providers.SqlConnectionProvider
{
    public interface ISqlConnectionProvider
    {
        public Task ExecuteAsync<T>(string query, T entity);
        public Task<IEnumerable<T>> QueryAsync<T>(string query);
        public Task<T> ExecuteScalarAsync<T>(string query, object parameter);
        public Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameter);
    }
}
