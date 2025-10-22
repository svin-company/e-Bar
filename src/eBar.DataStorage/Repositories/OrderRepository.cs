using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories.Interfaces;
using eBar.Core.Model;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using eBar.DataStorage.Exceptions;
using System.Transactions;
using Dapper;
using Npgsql;
using System.Collections;


namespace eBar.DataStorage.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfigReader _configReader;

        public OrderRepository(IConfigReader configReader)
        {
            _configReader = configReader;
        }

        public async Task<int> AddAsync(int orderStatusId, int tableId)
        {
            var connectionString = _configReader.GetConnectionString();
            DateTime orderTime = DateTime.Now;
            var query = @"INSERT INTO public.restaurant_order (order_time, order_status_id, restaurant_table_id)
                    VALUES (@orderTime, @orderStatusId, @tableId)
                    RETURNING id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<int>(query, new { orderTime, orderStatusId, tableId });
            }
        }


        public async Task<int> AddAsync(Order order)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"INSERT INTO public.restaurant_order (order_time, order_status_id, restaurant_table_id)
                    VALUES (@orderTime, @orderStatusId, @tableId)
                    RETURNING id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<int>(query, order);
            }
        }

        public async Task<Order> ChangeStatusAsync(Order order, bool orderStatus)
        {
            var connectionString = _configReader.GetConnectionString();
            int statusId = await GetStatusId(orderStatus);

            var updateQuery = @"UPDATE public.restaurant_order
                SET status_id = @statusId
                WHERE id =@Id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.ExecuteAsync(updateQuery, new { Id = order.Id, statusId = statusId });
            }
            return await GetByOrderIdWithStatus(order.Id);
        }

        public async Task<int> GetStatusId(bool orderStatus)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"SELECT id FROM public.order_status
                    WHERE is_order_open = @orderStatus;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<int>(query, new {orderStatus});
            }
        }

        public async Task AddOrderWithItemsAsync(Order order, int tableId)
        {
            var orderQuery = @"INSERT INTO public.restaurant_order (order_time, status_id, table_id)
                VALUES (@OrderTime, @OrderStatusId, @TableId)
                RETURNING id;";
            var orderItemQuery = @"INSERT INTO public.order_item (amount, food_id, restaurant_order_id) 
                        VALUES (@Amount, @FoodId, @OrderId);"
            ;

            var connection = new NpgsqlConnection(_configReader.GetConnectionString());
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                Console.WriteLine($"Connection state: {connection.State}");
                int orderId = await connection.ExecuteScalarAsync<int>(orderQuery,
                    new { OrderTime = order.OrderTime, OrderStatusId = order.OrderStatusId, TableId = tableId },
                    transaction);

                order.Id = orderId;
                foreach (var item in order.OrderItems)
                {
                    item.OrderId = order.Id;
                    await connection.ExecuteAsync(orderItemQuery, new
                    {
                        Amount = item.Amount,
                        FoodId = item.Food.Id,
                        OrderId = order.Id
                    }, transaction);
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionErrorException("Ошибка добавления заказа с товарами");
            }
            finally
            {
                await connection.CloseAsync();
            }

        }

        public async Task DeleteAsync(int id)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"DELETE from public.restaurant_order
                WHERE id =@id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var connectionString = _configReader.GetConnectionString();
            var query = "SELECT * FROM public.restaurant_order";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QueryAsync<Order>(query);
            }
        }

        public async Task<Order> GetByOrderIdAsync(int id)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"SELECT * FROM public.restaurant_order
                WHERE id = @Id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Order>(query, new { id });
            }
        }

        public async Task<Order> GetByOrderIdWithStatus(int id)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"SELECT o.id,
                o.order_time,
                o.status_id AS orderstatusid,
                o.table_id,
                s.is_order_open AS isOrderOpen
                FROM public.restaurant_order o
                JOIN public.order_status s ON o.status_id = s.id
                WHERE o.id = @id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Order>(query, new { id });
            }
        }

        public async Task<IEnumerable<Order>> GetByTableIdAsync(int id)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"SELECT o.id,
                o.order_time,
                o.status_id AS orderstatusid,
                o.table_id,
                s.is_order_open AS isOrderOpen
                FROM public.restaurant_order o
                JOIN public.order_status s ON o.status_id = s.id
                WHERE o.table_id = @id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QueryAsync<Order>(query, new { id });
            }
        }
        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"SELECT 
                i.id AS order_item_id,
                i.amount AS Amount,
                i.food_id AS FoodId,
                i.restaurant_order_id AS OrderId,
                f.id AS food_id,
                f.name AS Name,
                f.price AS Price
                FROM public.order_item i
                JOIN public.food f ON i.food_id = f.id
                WHERE i.restaurant_order_id = @orderId;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<OrderItem, Food, OrderItem>(
                    query,
                    (item, food) =>
                    {
                        item.Food = food;
                        return item;
                    },
                    new { orderId },
                    splitOn: "food_id"
                );
                return result;
            }
        }
    }
}
