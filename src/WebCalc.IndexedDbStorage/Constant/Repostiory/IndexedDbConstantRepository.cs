using Blazor.IndexedDB.WebAssembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.Constant;
using WebCalc.Domain.Constant.DomainManager;
using WebCalc.Domain.Repositories;
using WebCalc.IndexedDbStorage.Constant.Model;
using WebCalc.IndexedDbStorage.Data;

namespace WebCalc.IndexedDbStorage.Constant.Repostiory
{
    public class IndexedDbConstantRepository : IConstantRepository
    {
        private readonly IIndexedDbFactory indexedDbFactory;

        public IndexedDbConstantRepository(IIndexedDbFactory indexedDbFactory)
        {
            this.indexedDbFactory = indexedDbFactory;
        }

        public async Task CreateAsync(Domain.Constant.Constant constant)
        {
            using var db = await indexedDbFactory.Create<WebCalcDb>();
            db.Constants.Add(new ()
            {
                Id = constant.Id,
                Name = constant.Name,
                Value = constant.Value,
                Description = constant.Description,
            });
            await db.SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            using var db = await indexedDbFactory.Create<WebCalcDb>();
            var constant = db.Constants.Single(x => x.Id == id);
            db.Constants.Remove(constant);
            await db.SaveChanges();
        }

        public async Task<IEnumerable<Domain.Constant.Constant>> GetAllAsync()
        {
            var result = new List<Domain.Constant.Constant>();
            using var db = await indexedDbFactory.Create<WebCalcDb>();
            var constants = db.Constants;

            foreach (var constant in db.Constants)
            {
                var @const = Activator.CreateInstance(typeof(Domain.Constant.Constant), true)!;

                SetConstant(constant, @const);

                result.Add((Domain.Constant.Constant)@const);
            }

            return result;
        }

        public async Task<Domain.Constant.Constant> GetByIdAsync(Guid id)
        {
            using var db = await indexedDbFactory.Create<WebCalcDb>();
            var constant = db.Constants.Single(x => x.Id == id);
            var result = Activator.CreateInstance(typeof(Domain.Constant.Constant), true);

            SetConstant(constant, result);

            return (Domain.Constant.Constant)result;
        }

        public async Task UpdateAsync(Guid id, Domain.Constant.Constant constant)
        {
            using var db = await indexedDbFactory.Create<WebCalcDb>();
            var @const = db.Constants.Single(x => x.Id == id);

            @const.Name = constant.Name;
            @const.Value = constant.Value;
            @const.Description = constant.Description;

            await db.SaveChanges();
        }

        private void SetConstant(ConstantModel constantModel, object constantDomain)
        {
            var idProperty = typeof(Domain.Constant.Constant).GetProperty("Id");
            var nameProperty = typeof(Domain.Constant.Constant).GetProperty("Name");
            var valueProperty = typeof(Domain.Constant.Constant).GetProperty("Value");
            var descriptionProperty = typeof(Domain.Constant.Constant).GetProperty("Description");

            idProperty.SetValue(constantDomain, constantModel.Id);
            nameProperty.SetValue(constantDomain, constantModel.Name);
            valueProperty.SetValue(constantDomain, constantModel.Value);
            descriptionProperty?.SetValue(constantDomain, constantModel.Description);
        }
    }
}
