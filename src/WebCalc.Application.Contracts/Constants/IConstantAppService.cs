using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Application.Contracts.Constants.DTO;

namespace WebCalc.Application.Contracts.Constants
{
    public interface IConstantAppService
    {
        public Task<ConstantDto> CreateAsync(CreateConstantDto constantDto);

        public Task<ConstantDto> UpdateAsync(Guid id, UpdateConstantDto constantDto);

        public Task DeleteAsync(Guid id);

        public Task<IEnumerable<ConstantDto>> GetAllAsync();
    }
}
