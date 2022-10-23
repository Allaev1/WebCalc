using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.Constant.DomainManager
{
    public interface IConstantManager
    {
        public Task<Constant> CreateConstantAsync(string name, float value, string description);

        public Task<Constant> UpdateConstantAsync(Guid id, string name, float value, string description);
    }
}
