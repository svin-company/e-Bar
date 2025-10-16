using eBar.DataStorage.Providers.SqlConnectionProvider;
using eBar.DataStorage.Reader;
using eBar.Core.Model;

namespace eBar.DataStorage.Repositories
{
    public class TableRepository
    {
        private readonly ISqlConnectionProvider _sqlConnectionProvider;

        public TableRepository(ISqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

        public async Task<IEnumerable<Table>> GetAll()
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = "SELECT * FROM public.table";
            return await _sqlConnectionProvider.QueryAsync<Table>(query);
        }
    }
}
