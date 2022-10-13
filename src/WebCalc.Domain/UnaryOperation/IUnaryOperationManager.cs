using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.UnaryOperation
{
    public interface IUnaryOperationManager
    {
        public UnaryOperation Percentage { get; } 

        public UnaryOperation Negate { get; }
    }
}
