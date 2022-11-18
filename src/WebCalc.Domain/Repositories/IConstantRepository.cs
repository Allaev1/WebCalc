using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.Repositories
{
    public interface IConstantRepository
    {
        public Task<IEnumerable<Constant.Constant>> GetAllAsync();

        public Task<Constant.Constant> GetByIdAsync(Guid id);

        public Task CreateAsync(Constant.Constant constant);

        public Task DeleteAsync(Guid id);

        public Task UpdateAsync(Guid id, Constant.Constant constant);
    }
}
