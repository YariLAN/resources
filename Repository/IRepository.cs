using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T> where T : class
    {
        public Task<int> CreateAsync(T entity);

        public Task<bool> UpdateAsync(T entity);

        public Task<bool> DeleteAsync(int id);

        public Task<List<T>> GetValuesAsync();

        public Task<T> GetByIdAsync(int id);
    }
}
