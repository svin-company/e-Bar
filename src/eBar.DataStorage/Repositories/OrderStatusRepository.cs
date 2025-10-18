using eBar.DataStorage.Providers.SqlConnectionProvider;
using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace eBar.DataStorage.Repositories
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly ISqlConnectionProvider _sqlConnectionProvider;

        public OrderStatusRepository(ISqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

        public async Task<int> Add(string name)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"INSERT INTO public.order_status (name)
                    VALUES (@Name)
                    RETURNING id;";
            return await _sqlConnectionProvider
                .ExecuteScalarAsync<int>(query, new {Name = name});
        }

        public async Task Update(OrderStatus status)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"UPDATE public.order_status
                SET name = @Name
                WHERE id =@Id;";
            await _sqlConnectionProvider.ExecuteAsync(query, status);
        }

        public async Task Delete(int id)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"DELETE from public.order_status
                WHERE id =@id;";
            await _sqlConnectionProvider.ExecuteAsync(query, new { id });
        }

        public async Task<OrderStatus> Get(string name)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"SELECT * FROM public.order_status
                WHERE name = @Name";
            return await _sqlConnectionProvider
                .QuerySingleOrDefaultAsync<OrderStatus>(query, new {Name = name});
        }

        public async Task<IEnumerable<OrderStatus>> GetAll()
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = "SELECT * FROM public.order_status";
            return await _sqlConnectionProvider.QueryAsync<OrderStatus>(query);
        }

  
    }
}
