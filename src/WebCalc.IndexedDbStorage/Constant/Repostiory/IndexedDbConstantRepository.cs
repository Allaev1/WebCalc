using WebCalc.Domain.Repositories;
using WebCalc.IndexedDbStorage.Data;

namespace WebCalc.IndexedDbStorage.Constant.Repostiory
{
    public class IndexedDbRepository<T> : IRepository<T>
    {
        private readonly WebCalcDb webCalcDb;

        public IndexedDbRepository(WebCalcDb webCalcDb)
        {
            this.webCalcDb = webCalcDb;
        }

        public async Task CreateAsync(T entity)
        {
            await webCalcDb.AddItems(new List<T> { entity });
        }

        public async Task DeleteAsync(Guid id)
        {
            await webCalcDb.DeleteByKey<Guid, T>(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await webCalcDb.GetAll<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await webCalcDb.GetByKey<Guid, T>(id);
        }

        public async Task UpdateAsync(Guid id, T entity)
        {
            await webCalcDb.UpdateItems(new List<T> { entity });
        }
    }
}
