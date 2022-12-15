using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.Constant.Proxy
{
    /// <summary>
    /// Use for desirializtion when storing data in IndexedDb
    /// </summary>
    public class ConstantProxy
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public float Value { get; set; }

        public string? Description { get; set; } = null!;

        public Domain.Constant.Constant GetDomainModelEquivalent()
        {
            var constant = (Domain.Constant.Constant)Activator.CreateInstance(typeof(Domain.Constant.Constant), true)!;

            var idProperty = typeof(Domain.Constant.Constant).GetProperty(nameof(constant.Id))!;
            var nameProperty = typeof(Domain.Constant.Constant).GetProperty(nameof(constant.Name))!;
            var valueProperty = typeof(Domain.Constant.Constant).GetProperty(nameof(constant.Value))!;
            var descriptionProperty = typeof(Domain.Constant.Constant).GetProperty(nameof(constant.Description))!;

            idProperty.SetValue(constant, Id);
            nameProperty.SetValue(constant, Name);
            valueProperty.SetValue(constant, Value);
            descriptionProperty.SetValue(constant, Description);

            return constant;
        }

        public static ConstantProxy ToProxyModel(Domain.Constant.Constant constant)
        {
            return new()
            {
                Id = constant.Id,
                Name = constant.Name,
                Value = constant.Value,
                Description = constant.Description,
            };
        }
    }
}
