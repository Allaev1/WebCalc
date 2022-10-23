using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.Constant.Exceptions;
using WebCalc.Domain.Repositories;

namespace WebCalc.Domain.Constant.DomainManager
{
    public class ConstantManager : IConstantManager
    {
        private readonly IConstantRepository constantRepository;

        public ConstantManager(IConstantRepository constantRepository)
        {
            this.constantRepository = constantRepository;
        }

        public async Task<Constant> CreateConstantAsync(string name, float value, string description)
        {
            await ValidateName(name);

            var constant = new Constant(name, value, description);

            return constant;
        }

        public async Task<Constant> UpdateConstantAsync(Guid id, string name, float value, string description)
        {
            var @const = await constantRepository.GetByIdAsync(id);

            if (@const is null)
            {
                throw new ConstantNotFoundException(id.ToString());
            }

            await ValidateName(name, id);

            @const.SetName(name);
            @const.Value = value;
            @const.Description = description;

            return @const;
        }

        private async Task ValidateName(string name, Guid? id = null)
        {
            var nameDuplicated = (await constantRepository.GetAllAsync()).Any(x => x.Name == name && x.Id != id);

            if (nameDuplicated)
            {
                throw new ConstantNameDuplicatedException(name);
            }
        }
    }
}
