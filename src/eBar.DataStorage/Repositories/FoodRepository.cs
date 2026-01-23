using Dapper;
using eBar.Configuration;
using eBar.Core.Model;
using eBar.DataStorage.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly DbConfigReader _dbConfigReader;

        public FoodRepository(DbConfigReader dbConfigReader)
        {
            _dbConfigReader = dbConfigReader;
        }

        public async Task<int> AddAsync(string name, decimal price)
        {
            var connectionString = _dbConfigReader.Connection;
            var query = @"INSERT INTO public.food (name, price)
                    VALUES (@Name, @Price)
                    RETURNING id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<int>(query, new { Name = name, Price = price });
            }
        }

        public async Task UpdateAsync(Food food)
        {
            var connectionString = _dbConfigReader.Connection;
            var query = @"UPDATE public.food
                SET name = @Name, price =@Price 
                WHERE id =@Id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, food);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var connectionString = _dbConfigReader.Connection;
            var query = @"DELETE from public.food
                WHERE id =@id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Food> GetAsync(string name)
        {
            var connectionString = _dbConfigReader.Connection;
            var query = @"SELECT * FROM public.food
                WHERE name = @Name";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Food>(query, new { Name = name });
            }
        }

        public async Task<IEnumerable<Food>> GetAllAsync()
        {
            var connectionString = _dbConfigReader.Connection;
            var query = "SELECT * FROM public.food";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QueryAsync<Food>(query);
            }
        }
    }
}
