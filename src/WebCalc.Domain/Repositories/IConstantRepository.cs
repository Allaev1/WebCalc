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

        public Task<Constant.Constant> CreateAsync(string name, float value, string? description = null);

        public Task DeleteAsync(Guid id);

        public Task<Constant.Constant> UpdateAsync(Guid id, Constant.Constant value);
    }
}
