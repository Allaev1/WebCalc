using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.Repositories
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(Guid id);

        public Task CreateAsync(T entity);

        public Task DeleteAsync(Guid id);

        public Task UpdateAsync(Guid id, T entity);
    }
}
