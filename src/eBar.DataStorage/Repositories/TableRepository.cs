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
        private readonly DbConfigReader _dbConfigReader;

        public TableRepository(DbConfigReader dbConfigReader)
        {
            _dbConfigReader = dbConfigReader;
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
        {
            var query = "SELECT * FROM public.restaurant_table";
            await using (var connection = new NpgsqlConnection(_dbConfigReader.Connection))
            {
                return await connection.QueryAsync<Table>(query);
            }
        }
    }
}
