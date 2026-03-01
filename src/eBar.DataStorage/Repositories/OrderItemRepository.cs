using Dapper;
using eBar.Core.Model;
using eBar.DataStorage.Exceptions;
using eBar.DataStorage.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DbConfigReader _dbConfigReader;

        public OrderItemRepository(DbConfigReader dbConfigReader)
        {
            _dbConfigReader = dbConfigReader;
        }

        public async Task AddAsync(OrderItem item)
        {
            var query = @"INSERT INTO public.order_item (amount, food_id, restaurant_order_id) 
                        VALUES (@Amount, @FoodId, @OrderId);";

            await using (var connection = new NpgsqlConnection(_dbConfigReader.Connection))
            {
                await connection.ExecuteScalarAsync<int>(query, item);
            }
        }
        public async Task <bool> UpdateItemsAsync(List<OrderItem> items)
       
        {
            var connection = new NpgsqlConnection(_dbConfigReader.Connection);

            var createTempQuery = @"CREATE TEMPORARY TABLE 
                    pg_temp.temp_items (
                    amount INT, 
                    food_id INT,
                    restaurant_order_id INT
                    );";

            var insertTempQuery = @"INSERT INTO pg_temp.temp_items (amount, food_id, restaurant_order_id)
                VALUES (@Amount, @FoodId, @OrderId)";

            var deleteQuery = @"DELETE FROM public.order_item oi
                WHERE oi.restaurant_order_id =@OrderId
                AND NOT EXISTS (
                    SELECT 1
                    FROM pg_temp.temp_items ti
                    WHERE ti.food_id = oi.food_id
                    AND ti.restaurant_order_id = oi.restaurant_order_id
                    );";
            var updateQuery = @"UPDATE public.order_item oi
                SET amount =@Amount
                WHERE oi.food_id = @FoodId AND oi.restaurant_order_id =@OrderId";

            var insertQuery = @"INSERT INTO public.order_item (amount, food_id, restaurant_order_id)
                VALUES (@Amount, @FoodId, @OrderId)
                ON CONFLICT (food_id, restaurant_order_id)
                DO NOTHING";

            var result = false;
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                await connection.ExecuteAsync(createTempQuery, transaction: transaction);
                var v = await connection.ExecuteAsync(insertTempQuery, items, transaction: transaction);
                var c = await connection.ExecuteAsync(deleteQuery, items, transaction: transaction);

                var d = await connection.ExecuteAsync(updateQuery, items, transaction);
                var b = await connection.ExecuteAsync(insertQuery, items, transaction);

                await transaction.CommitAsync();
                result = true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionErrorException("Ошибка обновления заказа");
            }
            finally
            {
                await connection.CloseAsync();
            }

            return result;
        }
        public async Task DeleteAsync(int id)
        {
            var query = @"DELETE * FROM public.order_item
                WHERE id = @id";
            await using (var connection = new NpgsqlConnection(_dbConfigReader.Connection))
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            var query = "SELECT * FROM public.order_item";
            await using (var connection = new NpgsqlConnection(_dbConfigReader.Connection))
            {
                return await connection.QueryAsync<OrderItem>(query);
            }
        }
        public async Task<IEnumerable<OrderItem>> GetItemsByOrderIdAsync(int orderId)
        {
            var query = @"SELECT 
                i.id AS Id,
                i.amount AS Amount,
                i.food_id AS FoodId,
                i.restaurant_order_id AS OrderId,

                f.id AS Id,
                f.name AS Name,
                f.price AS Price
                FROM public.order_item i
                JOIN public.food f ON i.food_id = f.id
                WHERE i.restaurant_order_id = @orderId;";

            await using (var connection = new NpgsqlConnection(_dbConfigReader.Connection))
            {
                var result = await connection.QueryAsync<OrderItem, Food, OrderItem>(
                    query,
                    (item, food) =>
                    {
                        item.Food = food;
                        item.FoodId = food.Id;
                        return item;
                    },
                    new { orderId },
                    splitOn: "Id"
                );
                return result;
            }
        }

    }
}
