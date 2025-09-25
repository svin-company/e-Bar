using Dapper;
using eBar.DataStorage.ConfigReader;
using eBar.DataStorage.Providers.EntityAttributeProvider;
using Npgsql;

namespace eBar.DataStorage.Providers.SqlConnectionProvider
{
    public class SqlConnectionProvider<T> : ISqlConnectionProvider<T> where T : class
    {
        private readonly IConfigReader _configReader;
        private readonly IEntityAttributeProvider _entityAttributeProvider;

        public SqlConnectionProvider(IConfigReader configReader, IEntityAttributeProvider entityAttributeProvider)
        {
            _configReader = configReader;
            _entityAttributeProvider = entityAttributeProvider;
        }

        public async Task<int> AddAsync(T entity)
        {
            await using (var connection = new NpgsqlConnection(_configReader.GetConnectionString()))
            {
                var tableName = _entityAttributeProvider.GetTableName<T>();
                var columnPropertyNames = _entityAttributeProvider.GetColumnAndModelPropertyNames<T>(addKey: false);
                var query = $@"
                    INSERT INTO {tableName} ({string.Join(", ", columnPropertyNames.Keys)})
                    VALUES ({string.Join(", ", columnPropertyNames.Values.Select(propName => $"@{propName}"))})
                    RETURNING id;";

                int id = await connection.QuerySingleAsync<int>(query, entity);
                return id;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using (var connection = new NpgsqlConnection(_configReader.GetConnectionString()))
            {
                var tableName = _entityAttributeProvider.GetTableName<T>();
                (string keyColumnName, string keyPropertyName) = _entityAttributeProvider.GetKeyColumnAndPropertyName<T>();
                var query = $"DELETE FROM {tableName} WHERE {keyColumnName} = @{keyPropertyName}";
                var parameters = new DynamicParameters();
                parameters.Add($"{keyColumnName}", id);

                return await connection.ExecuteAsync(query, parameters) > 0;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            await using (var connection = new NpgsqlConnection(_configReader.GetConnectionString()))
            {
                var tableName = _entityAttributeProvider.GetTableName<T>();
                var columnPropertyNames = _entityAttributeProvider.GetColumnAndModelPropertyNames<T>();
                string formatedNames = _entityAttributeProvider.
                    GetFormattedColumnAndModelPropertyNames<T>(columnPropertyNames, "{0} AS {1}");

                var query = $"SELECT {formatedNames} FROM {tableName}";
                return await connection.QueryAsync<T>(query);
            }
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            await using (var connection = new NpgsqlConnection(_configReader.GetConnectionString()))
            {
                var tableName = _entityAttributeProvider.GetTableName<T>();
                var columnPropertyNames = _entityAttributeProvider.GetColumnAndModelPropertyNames<T>();
                string formatedNames = _entityAttributeProvider.
                    GetFormattedColumnAndModelPropertyNames<T>(columnPropertyNames, "{0} AS {1}");
                (string keyColumnName, string keyPropertyName) = _entityAttributeProvider.GetKeyColumnAndPropertyName<T>();

                var query = $"SELECT {formatedNames} FROM {tableName} WHERE {keyColumnName} = @{keyPropertyName}";
                var parameters = new DynamicParameters();
                parameters.Add($"{keyColumnName}", id);

                return await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            await using (var connection = new NpgsqlConnection(_configReader.GetConnectionString()))
            {
                var tableName = _entityAttributeProvider.GetTableName<T>();
                var columnPropertyNames = _entityAttributeProvider.GetColumnAndModelPropertyNames<T>(addKey: false);
                (string keyColumnName, string keyPropertyName) = _entityAttributeProvider.GetKeyColumnAndPropertyName<T>();
                string columnsAndProperties = _entityAttributeProvider.GetFormattedColumnAndModelPropertyNames<T>(columnPropertyNames, "{0} = @{1}");

                var query = $"UPDATE {tableName} SET {columnsAndProperties} WHERE {keyColumnName} = @{keyPropertyName}";
                return await connection.ExecuteAsync(query, entity) > 0;
            }
        }
    }
}
