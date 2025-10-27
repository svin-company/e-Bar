using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Npgsql;
using Dapper;

namespace eBar.DataStorage.Repositories
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly IConfigReader _configReader;

        public OrderStatusRepository(IConfigReader configReader)
        {
            _configReader = configReader;
        }

        public async Task<int> AddAsync(string name)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"INSERT INTO public.order_status (name)
                    VALUES (@Name)
                    RETURNING id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<int>(query, new { Name = name });
            }
        }

        public async Task UpdateAsync(OrderStatus status)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"UPDATE public.order_status
                SET name = @Name
                WHERE id =@Id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, status);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"DELETE from public.order_status
                WHERE id =@id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<OrderStatus> GetAsync(string name)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"SELECT * FROM public.order_status
                WHERE name = @Name";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<OrderStatus>(query, new { Name = name });
            }
        }

        public async Task<IEnumerable<OrderStatus>> GetAllAsync()
        {
            var connectionString = _configReader.GetConnectionString();
            var query = "SELECT * FROM public.order_status";
            await using (var connection = new NpgsqlConnection(_configReader.GetConnectionString()))
            {
                return await connection.QueryAsync<OrderStatus>(query);
            }
        }

  
    }
}
