namespace WebCalc.Application.Contracts.Services.Settings
{
    public interface ISettings
    {
        public Task CreateAsync<T>(string name, T value);

        public Task DeleteAsync(string name);

        public Task UpdateAsync<T>(string name, T value);

        public Task<T> GetAsync<T>(string name);
    }
}
