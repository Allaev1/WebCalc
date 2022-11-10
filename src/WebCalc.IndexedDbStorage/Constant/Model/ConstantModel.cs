using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.IndexedDbStorage.Constant.Model
{
    public class ConstantModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public float Value { get; set; }

        public string? Description { get; set; } = null!;
    }
}
