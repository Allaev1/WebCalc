using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.Constant.Exceptions;
using WebCalc.Domain.Shared.Constant;

namespace WebCalc.Domain.Constant
{
    public class Constant : ConstantBase<float>
    {
        internal Constant(
            string name,
            float value,
            string? description):base(name, value)
        {
            Description = description;
        }

        public string? Description { get; set; }
    }
}
