using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.Constant;
using WebCalc.Domain.Repositories;

namespace WebCalc.BrowserLocalStorageAccess.Repositories
{
    public class ConstantRepository : IConstantRepository
    {
        public Task<Constant> CreateAsync(string name, float value, string? description = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Constant>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Constant> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Constant> UpdateAsync(Guid id, Constant value)
        {
            throw new NotImplementedException();
        }
    }
}
