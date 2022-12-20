using WebCalc.Domain.Repositories;
using WebCalc.IndexedDbStorage.Data;

namespace WebCalc.IndexedDbStorage.Constant.Repostiory
{
    public class IndexedDbRepository<T> : IRepository<T>
    {
        private readonly WebCalcDb webCalcDb;
        private readonly string objectStoreName;
        private const char PluralEnding = 's';

        public IndexedDbRepository(WebCalcDb webCalcDb)
        {
            this.webCalcDb = webCalcDb;
            objectStoreName = $"{typeof(T).Name.Replace("Proxy", string.Empty)}{PluralEnding}".ToLower();
        }

        public async Task CreateAsync(T entity)
        {
            await webCalcDb.AddItems(objectStoreName, new List<T> { entity });
        }

        public async Task DeleteAsync(Guid id)
        {
            await webCalcDb.DeleteByKey<Guid>(objectStoreName, id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            await webCalcDb.OpenIndexedDb();
            return await webCalcDb.GetAll<T>(objectStoreName);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await webCalcDb.GetByKey<Guid, T>(objectStoreName, id);
        }

        public async Task UpdateAsync(Guid id, T entity)
        {
            await webCalcDb.UpdateItems(objectStoreName, new List<T> { entity });
        }
    }
}
