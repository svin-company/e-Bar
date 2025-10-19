using Dapper;
using eBar.Core.Model;
using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly IConfigReader _configReader;

        public FoodRepository(IConfigReader configReader)
        {
            _configReader = configReader;
        }

        public async Task<int> AddAsync(string name, decimal price)
        {
            var connectionString = _configReader.GetConnectionString();
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
            var connectionString = _configReader.GetConnectionString();
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
            var connectionString = _configReader.GetConnectionString();
            var query = @"DELETE from public.food
                WHERE id =@id;";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Food> GetAsync(string name)
        {
            var connectionString = _configReader.GetConnectionString();
            var query = @"SELECT * FROM public.food
                WHERE name = @Name";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Food>(query, new { Name = name });
            }
        }

        public async Task<IEnumerable<Food>> GetAllAsync()
        {
            var connectionString = _configReader.GetConnectionString();
            var query = "SELECT * FROM public.food";
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QueryAsync<Food>(query);
            }
        }
    }
}
