using Dapper;
using eBar.Core.Model;
using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eBar.DataStorage.Repositories
{
    public class WaiterRepository : IWaiterRepository
    {
        private readonly IConfigReader _configReader;

        public WaiterRepository(IConfigReader configReader)
        {
            _configReader = configReader;
        }

        public async Task<IEnumerable<Waiter>> GetAllAsync()
        {
            var connectionString = _configReader.GetConnectionString();
            var query = "SELECT * FROM public.waiter";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QueryAsync<Waiter>(query);
            }
        }

        public async Task<string> GetByIdAsync(int waiterId)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"SELECT name from public. waiter
                WHERE id = @Id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<string>(query, new { Id = waiterId });
            }
        }
    }
}
