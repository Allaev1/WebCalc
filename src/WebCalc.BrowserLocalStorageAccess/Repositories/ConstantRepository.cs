using Blazored.LocalStorage;
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
        private readonly ILocalStorageService localStorageService;

        public ConstantRepository(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task CreateAsync(Constant constant)
        {
            await localStorageService.SetItemAsync(constant.Id.ToString(), constant);
        }

        public async Task DeleteAsync(Guid id)
        {
            await localStorageService.RemoveItemAsync(id.ToString());
        }

        public async Task<IEnumerable<Constant>> GetAllAsync()
        {
            List<Constant> constants = new();
            var keys = await localStorageService.KeysAsync();

            foreach (var key in keys)
            {
                var constant = await localStorageService.GetItemAsync<Constant>(key);
                constants.Add(constant);
            }

            return constants;
        }

        public async Task<Constant> GetByIdAsync(Guid id)
        {
            var constant = await localStorageService.GetItemAsync<Constant>(id.ToString());

            return constant;
        }

        public async Task UpdateAsync(Guid id, Constant constant)
        {
            await localStorageService.RemoveItemAsync(id.ToString());

            await localStorageService.SetItemAsync(id.ToString(), constant);
        }
    }
}
