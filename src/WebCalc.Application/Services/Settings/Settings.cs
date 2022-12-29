using Blazored.LocalStorage;
using WebCalc.Application.Contracts.Services.Settings;

namespace WebCalc.Application.Services.Settings
{
    public class Settings : ISettings
    {
        private readonly ILocalStorageService localStorage;

        public Settings(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task CreateAsync<T>(string name, T value)
        {
            await localStorage.SetItemAsync(name, value);
        }

        public async Task DeleteAsync(string name)
        {
            await localStorage.RemoveItemAsync(name);
        }

        public async Task<T> GetAsync<T>(string name)
        {
            return await localStorage.GetItemAsync<T>(name);
        }

        public async Task UpdateAsync<T>(string name, T value)
        {
            await localStorage.RemoveItemAsync(name);
            await localStorage.SetItemAsync(name, value);
        }

        public async Task<bool> IsSettingExistAsync(string name)
        {
            return await localStorage.ContainKeyAsync(name);
        }
    }
}
