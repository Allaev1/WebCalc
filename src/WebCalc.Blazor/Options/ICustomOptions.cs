namespace WebCalc.Blazor.Options
{
    public interface ICustomOptions
    {
        public Task CreateAsync<T>(string name, T value);

        public Task DeleteAsync(string name);

        public Task UpdateAsync<T>(string name, T value);

        public Task<T> GetAsync<T>(string name);
    }
}
