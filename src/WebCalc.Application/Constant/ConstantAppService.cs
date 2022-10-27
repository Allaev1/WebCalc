using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Application.Contracts.Constants;
using WebCalc.Application.Contracts.Constants.DTO;
using WebCalc.Domain.Constant.DomainManager;
using WebCalc.Domain.Repositories;

namespace WebCalc.Application.Constant
{
    public class ConstantAppService : IConstantAppService
    {
        private readonly IConstantManager constantManager;
        private readonly IConstantRepository constantRepository;

        public ConstantAppService(IConstantManager constantManager, IConstantRepository constantRepository)
        {
            this.constantManager = constantManager;
            this.constantRepository = constantRepository;
        }

        public async Task<ConstantDto> CreateAsync(CreateConstantDto constantDto)
        {
            var constant = await constantManager.CreateConstantAsync(constantDto.Name, constantDto.Value, constantDto.Description);

            await constantRepository.CreateAsync(constant);

            return new()
            {
                Id = constant.Id,
                Name = constant.Name,
                Value = constant.Value,
                Description = constant.Description,
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            await constantRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ConstantDto>> GetAllAsync()
        {
            var constants = await constantRepository.GetAllAsync();

            return constants.Select(x=>new ConstantDto
            {
                Id = x.Id,
                Name = x.Name,
                Value = x.Value,
                Description = x.Description,
            });
        }

        public async Task<ConstantDto> UpdateAsync(Guid id, UpdateConstantDto constantDto)
        {
            var constant = await constantManager.UpdateConstantAsync(id, constantDto.Name, constantDto.Value, constantDto.Description);

            await constantRepository.UpdateAsync(id, constant);

            return new()
            {
                Id = constant.Id,
                Name = constant.Name,
                Value = constant.Value,
                Description = constant.Description
            };
        }
    }
}
