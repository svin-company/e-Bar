using eBar.DataStorage.Repositories.Interfaces;
using eBar.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Npgsql;
using Dapper;

namespace eBar.DataStorage.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DbConfigReader _dbConfigReader;

        public OrderItemRepository(DbConfigReader dbConfigReader)
        {
            _dbConfigReader = dbConfigReader;
        }

      
        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            var connectionString = _dbConfigReader.Connection;
            var query = "SELECT * FROM public.order_item";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QueryAsync<OrderItem>(query);
            }
        }
    }
}
