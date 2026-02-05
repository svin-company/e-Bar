using Dapper;
using eBar.Core.Model;
using eBar.DataStorage.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eBar.DataStorage.Repositories
{
    public class WaiterRepository : IWaiterRepository
    {
        private readonly DbConfigReader _dbConfigReader;

        public WaiterRepository(DbConfigReader dbConfigReader)
        {
            _dbConfigReader = dbConfigReader;
        }

        public async Task<IEnumerable<Waiter>> GetAllAsync()
        {
            var query = "SELECT * FROM public.waiter";
            await using (var connection = new NpgsqlConnection(_dbConfigReader.Connection))
            {
                return await connection.QueryAsync<Waiter>(query);
            }
        }

        public async Task<string> GetByIdAsync(int waiterId)
        {
            var query = @"SELECT name from public. waiter
                WHERE id = @Id;";
            await using (var connection = new NpgsqlConnection(_dbConfigReader.Connection))
            {
                return await connection.ExecuteScalarAsync<string>(query, new { Id = waiterId });
            }
        }
    }
}
