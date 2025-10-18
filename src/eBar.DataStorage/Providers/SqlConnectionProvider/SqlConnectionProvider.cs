using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using eBar.DataStorage.Reader;
using Npgsql;

namespace eBar.DataStorage.Providers.SqlConnectionProvider
{
    public class SqlConnectionProvider: ISqlConnectionProvider
    {
        private readonly IConfigReader _configReader;

        public SqlConnectionProvider(IConfigReader configReader)
        {
            _configReader = configReader;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query)
        {
            await using (var connection = new NpgsqlConnection(_configReader.GetConnectionString()))
            {
                return await connection.QueryAsync<T>(query);
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string query, object parameters)
        {
            await using (var connection = new NpgsqlConnection(_configReader.GetConnectionString()))
            {
                return await connection.ExecuteScalarAsync<T>(query, parameters);
            }
        }

        public async Task ExecuteAsync(string query, object parameters)
        {
            await using (var connection = new NpgsqlConnection(_configReader.GetConnectionString()))
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameter)
        {
            await using (var connection = new NpgsqlConnection(_configReader.GetConnectionString()))
            {
                return await connection.QuerySingleOrDefaultAsync<T>(query, parameter);
            }
        }
    }
}
