using eBar.DataStorage.Reader;
using eBar.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using eBar.DataStorage.Repositories.Interfaces;
using Npgsql;
using Dapper;

namespace eBar.DataStorage.Repositories
{
    public class TableRepository: ITableRepository
    {
        private readonly IConfigReader _configReader;

        public TableRepository(IConfigReader configReader)
        {
            _configReader = configReader;
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
        {
            var connectionString = _configReader.GetConnectionString();
            var query = "SELECT * FROM public.restaurant_table";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QueryAsync<Table>(query);
            }
        }
    }
}
