using eBar.DataStorage.Providers.SqlConnectionProvider;
using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.Core.Model;

namespace eBar.DataStorage.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ISqlConnectionProvider _sqlConnectionProvider;

        public OrderRepository(ISqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

        public async Task<int> Add(int orderStatusId, int tableId)
        {
            DateTime orderTime = DateTime.Now;
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"INSERT INTO public.order (order_time, order_status_id, table_id)
                    VALUES (@orderTime, @orderStatusId, @tableId)
                    RETURNING id;";
            return await _sqlConnectionProvider
                .ExecuteScalarAsync<int>(query, new { orderTime, orderStatusId, tableId });
        }

        public async Task ChangeOrderStatus(Order order, int statusId)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"UPDATE public.order
                SET order_status_id=@orderId
                WHERE id =@Id;";
            await _sqlConnectionProvider.ExecuteAsync(query, order);
        }

        public async Task Delete(int id)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"DELETE from public.order
                WHERE id =@id;";
            await _sqlConnectionProvider.ExecuteAsync(query, new { id });
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = "SELECT * FROM public.order";
            return await _sqlConnectionProvider.QueryAsync<Order>(query);
        }

        public Task<Order> GetByOrderId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetByTableId(int id)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"SELECT * FROM public.order
                WHERE table_id =@id;";
            return await _sqlConnectionProvider.QueryAsync<Order>(query);
        }
    }
}
