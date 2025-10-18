using eBar.DataStorage.Providers.SqlConnectionProvider;
using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace eBar.DataStorage.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ISqlConnectionProvider _sqlConnectionProvider;

        public OrderItemRepository(ISqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

        public async Task<int> AddOrderItem(OrderItem item)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"INSERT INTO public.order_item (amount, food_id, order_id)
                    VALUES (@Amount, @FoodId, @OrderId)
                    RETURNING id;";
            return await _sqlConnectionProvider.ExecuteScalarAsync<int>(query, item);
        }

        public async Task Delete(int id)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"DELETE from public.order_item
                WHERE id =@id;";
            await _sqlConnectionProvider.ExecuteAsync(query, new { id });
        }

        public async Task<OrderItem> Get(int id)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"SELECT * FROM public.order_item
                WHERE id =@id;";
            return await _sqlConnectionProvider.QuerySingleOrDefaultAsync<OrderItem>(query, new {id});
        }

        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = "SELECT * FROM public.order_item";
            return await _sqlConnectionProvider.QueryAsync<OrderItem>(query);
        }

        public async Task<OrderItem> GetByFoodId(int id)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"SELECT * FROM public.order_item
                WHERE food_id=@id";
            return await _sqlConnectionProvider.QuerySingleOrDefaultAsync<OrderItem>(query, new {id});

        }

        public async Task IncreaseAmount(OrderItem orderItem, int amount)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"UPDATE public.order_item
	            SET amount=Amount
	            WHERE id = @orderItem.Id";
            await _sqlConnectionProvider.ExecuteAsync(query, new {Id = orderItem.Id, Amount = amount});
        }
    }
}
