﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBar.DataStorage.Providers.SqlConnectionProvider
{
    public interface ISqlConnectionProvider<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(int id);
        public Task<int> AddAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(int id);
    }
}
