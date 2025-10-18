using eBar.Core.Model;
using eBar.DataStorage.Providers.SqlConnectionProvider;
using eBar.DataStorage.Reader;
using eBar.DataStorage.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ISqlConnectionProvider _sqlConnectionProvider;

        public FoodRepository(ISqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

        public async Task<int> Add(string name, decimal price)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"INSERT INTO public.food (name, price)
                    VALUES (@Name, @Price)
                    RETURNING id;";
            return await _sqlConnectionProvider
                .ExecuteScalarAsync<int>(query, new { Name = name, Price = price});
        }

        public async Task Update(Food food)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"UPDATE public.food
                SET name = @Name, price =@Price 
                WHERE id =@Id;";
            await _sqlConnectionProvider.ExecuteAsync(query, food);
        }

        public async Task Delete(int id)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"DELETE from public.food
                WHERE id =@id;";
            await _sqlConnectionProvider.ExecuteAsync(query, new {id});
        }

        public async Task<Food> Get(string name)
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = @"SELECT * FROM public.food
                WHERE name = @Name";
            return await _sqlConnectionProvider
                .QuerySingleOrDefaultAsync<Food>(query, new {Name = name});
        }

        public async Task<IEnumerable<Food>> GetAll()
        {
            var _sqlConnectionProvider = new SqlConnectionProvider(new ConfigReader());
            var query = "SELECT * FROM public.food";
            return await _sqlConnectionProvider.QueryAsync<Food>(query);
        }
    }
}
